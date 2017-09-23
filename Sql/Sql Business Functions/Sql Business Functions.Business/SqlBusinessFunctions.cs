using Sql_Auto_Data_Discovery.Business.Models.Commom;
using Sql_Business_Functions.Business.Extentions;
using Sql_Business_Functions.Business.Helpers;
using Sql_Business_Functions.Business.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace Sql_Business_Functions.Business
{
    public class SqlBusinessFunctions : System.IDisposable
    {
        #region Fields
        private Script _script; 
        #endregion

        public Script Script
        {
            get { return _script ?? (_script = new Script()); }
        }

        public Result Load(string file)
        {
            try
            {
                _script = File.ReadAllText(file).ToObject<Script>();
                return Results.Success("Loaded");
            }
            catch (System.Exception e)
            {
                return e.ToResult();
            }
        }

        public Result Save(string file)
        {
            try
            {
                File.WriteAllText(file, Script.ToXml());
                return Results.Success("Saved");
            }
            catch (System.Exception e)
            {
                return e.ToResult();
            }
        }

        public List<Result> Run()
        {
            return Script.Actions.Select(Process_Action).ToList();
        }

        private Result Process_Action(SqlAction action)
        {
            if (action != null && action.Link != null)
            {
                //if sql not set build sql else proceed
                if (string.IsNullOrEmpty(action.Sql))
                {
                    switch (action.Link.Type)
                    {
                        case ActionType.Select:
                            action.Sql = Build_Sql_Select_Action(action.Link.Select);
                            break;
                        case ActionType.Insert:
                            action.Sql = Build_Sql_Insert_Action(action.Link.Insert);
                            break;
                        case ActionType.Update:
                            action.Sql = Build_Sql_Update_Action(action.Link.Update);
                            break;
                    }
                }
                //execute sql and return result ( be it dataset, datatable or return field from insert statement) 
                DataTable select_return_result = null;
                string insert_return_result_WithReturn = null;
                bool? insert_return_result;
                bool? update_return_result;
                using (var connection = new SqlConnection(new SqlConnectionStringBuilder
                    {
                        DataSource = action.Connection.Server_Ip_Or_Name,
                        InitialCatalog = action.Connection.Database_Name,
                        UserID = action.Connection.Username,
                        Password = action.Connection.Password,
                        IntegratedSecurity = action.Connection.Integrated_Security,
                    }.ConnectionString))
                {
                    if (action.Link != null)
                    {
                        if (connection.State != ConnectionState.Open) connection.Open();
                        switch (action.Link.Type)
                        {
                            case ActionType.Select:
                                select_return_result = Execute_Select_Sql(action, connection);
                                break;
                            case ActionType.Insert:
                                if (action.Link.Insert.Return_Column_Name.IsSet())
                                {
                                    insert_return_result_WithReturn = Execute_Insert_Sql_WithReturn(action, connection);
                                }
                                else
                                {
                                    insert_return_result = Execute_Insert_Sql(action, connection);
                                }
                                break;
                            case ActionType.Update:
                                update_return_result = Execute_Update_Sql(action, connection);
                                break;
                        }
                        connection.Close();
                    }
                }
                // if is select and select type not normal 
                //  > if select type is email then send email
                //  > if select type is file then save to file 
                Result result;
                if (action.Link != null && 
                    action.Link.Type == ActionType.Select && 
                    action.Link.Select.Type != ActionSelectType.Normal)
                {
                    switch (action.Link.Select.Type)
                    {
                        case ActionSelectType.File:
                            result = Process_File(action, select_return_result);
                            break;
                        case ActionSelectType.Email:
                            result = Process_Email(action, select_return_result);
                            break;
                        default: result = Results.Success();
                            break;
                    }
                }
                else
                {
                    result = Results.Success();
                }
                // if select type or (insert type and has return column) 
                //  > add result to globals
                if (action.Link != null)
                {
                    switch (action.Link.Type)
                    {
                        case ActionType.Select:
                            Globals.List.Add(new Global
                            {
                                Value = select_return_result,
                                ActionType = action.Link.Type,
                            });
                            break;
                        case ActionType.Insert:
                            if (action.Link.Insert.Return_Column_Name.IsSet())
                            {
                                Globals.List.Add(new Global
                                {
                                    Value = insert_return_result_WithReturn,
                                    ActionType = action.Link.Type,
                                });
                            }
                            break;
                    }
                }
                return result; 
            }
            else
            {
                return Results.Error("action or action link is null");
            }
        }

        #region Sql
        private string Build_Sql_Select_Action(SelectAction action)
        {
            try
            {
                return string.Format("select {0} from {1} {2} {3}"
                        , string.Join(", ", action.Columns)
                        , action.Table_Or_View_Name
                        , Build_Where_Sql(action.Filters)
                        , action.Order_By_Columns != null && action.Order_By_Columns.Any(o => o != null)
                            ? string.Format("order by {0}", string.Join(", ", action.Order_By_Columns))
                            : ""
                        );
            }
            catch (System.Exception e)
            {
                return "";
            }
        }
        private string Build_Sql_Insert_Action(InsertAction action)
        {
            try
            {
                return string.Format("insert into {0} ({1}) values ({2})"
                        , action.Table_Name
                        , string.Join(", ", action.Columns.Select(c => c.Column_Name).ToArray())
                        , Build_SetValues_Sql(action.Columns, seperator: ","));
            }
            catch (System.Exception e)
            {
                return "";
            }
        }
        private string Build_Sql_Update_Action(UpdateAction action)
        {
            try
            {
                return string.Format("update {0} set {1} where {2}"
                        , action.Table_Name
                        , Build_SetValues_Sql(action.Columns, seperator: "and")
                        , Build_Where_Sql(action.Filters));
            }
            catch (System.Exception e)
            {
                return "";
            }
        }

        private string Build_Where_Sql(Filter[] filters)
        {
            try
            {
                if (filters != null && filters.Any(f => f != null))
                {
                    var s = new StringBuilder("where ");
                    for (var index = 0; index < filters.Length; index++)
                    {
                        var filter = filters[index];
                        switch (filter.Type)
                        {
                            case FilterType.Equals: // = (Equals)
                                return string.Format("{0} = @{1}", filter.Column_Name, filter.Column_Name);
                            case FilterType.Greater_Than: // > (Greater Than)
                                return string.Format("{0} > @{1}", filter.Column_Name, filter.Column_Name);
                            case FilterType.Less_Than: // < (Less Than)
                                return string.Format("{0} < @{1}", filter.Column_Name, filter.Column_Name);
                            case FilterType.Greater_Than_Or_Equal_To: // >= (Greater Than or Equal To)
                                return string.Format("{0} >= @{1}", filter.Column_Name, filter.Column_Name);
                            case FilterType.Less_Than_Or_Equal_To: // <= (Less Than or Equal To)
                                return string.Format("{0} <= @{1}", filter.Column_Name, filter.Column_Name);
                            case FilterType.Not_Equal_To: // <> (Not Equal To)
                                return string.Format("{0} <> @{1}", filter.Column_Name, filter.Column_Name);
                            case FilterType.Not_Less_Than: // !< (Not Less Than) (not ISO standard)
                                return string.Format("{0} !< @{1}", filter.Column_Name, filter.Column_Name);
                            case FilterType.Not_Greater_Than: // !> (Not Greater Than) (not ISO standard)
                                return string.Format("{0} !> @{1}", filter.Column_Name, filter.Column_Name);
                            case FilterType.Like: // LIKE '%' + @value + '%' 
                                return string.Format("{0} like '%' + @{1} + '%'", filter.Column_Name, filter.Column_Name);
                            case FilterType.Not_Like: // NOT LIKE '%' + @value + '%' 
                                return string.Format("{0} not like '%' + @{1} + '%'", filter.Column_Name, filter.Column_Name);
                            case FilterType.Like_Right: // LIKE @value + '%' 
                                return string.Format("{0} like @{1} + '%'", filter.Column_Name, filter.Column_Name);
                            case FilterType.Not_Like_Right: // NOT LIKE @value + '%' 
                                return string.Format("{0} not like '%' + @{1} + '%'", filter.Column_Name, filter.Column_Name);
                            case FilterType.Like_Left: // LIKE '%' + @value 
                                return string.Format("{0} like '%' + @{1}", filter.Column_Name, filter.Column_Name);
                            case FilterType.Not_Like_Left: // NOT LIKE '%' + @value 
                                return string.Format("{0} not like '%' + @{1}", filter.Column_Name, filter.Column_Name);
                            case FilterType.Is_Null: // IS NULL  
                                return string.Format("{0} is null", filter.Column_Name);
                            case FilterType.Is_Not_Null: // IS NOT NULL  
                                return string.Format("{0} is not null", filter.Column_Name);
                            case FilterType.In: // IN (@values)  NOTE: not yet implemented
                            case FilterType.Not_In: // NOT IN (@values) NOTE: not yet implemented
                                break;
                        }
                        if (filters.Length != index)
                            s.Append(" and ");
                    }
                    return s.ToString();
                }
                else
                {
                    return "";
                }
            }
            catch (System.Exception e)
            {
                return "";
            }
        }
        private string Build_SetValues_Sql(Column[] columns, string seperator)
        {
            try
            {
                var builder = new StringBuilder();
                for (int index = 0; index < columns.Length; index++)
                {
                    var column = columns[index];
                    builder.Append(string.Format("{0} = @{1}", column.Column_Name, column.Column_Name));
                    if (columns.Length != index)
                        builder.AppendFormat("{0} ", seperator);
                }
                return builder.ToString();
            }
            catch (System.Exception e)
            {
                return "";
            }
        }
        #endregion

        #region Execute
        private DataTable Execute_Select_Sql(SqlAction action, SqlConnection connection)
        {
            try
            {
                var dt = new DataTable();
                using (var command = new SqlCommand(action.Sql, connection))
                {
                    if (action.Link.Select.Filters != null && action.Link.Select.Filters.Any(i => i != null))
                    {
                        foreach (var filter in action.Link.Select.Filters)
                        {
                            var sqlParameterName = filter.Column_Name.StartsWith("@")
                                ? filter.Column_Name
                                : string.Format("@{0}", filter.Column_Name);
                            command.Parameters.AddWithValue(sqlParameterName, filter.Value);
                        }
                    }
                    new SqlDataAdapter(command).Fill(dt);
                }
                return dt;
            }
            catch (System.Exception e)
            {
                return new DataTable();
            }
        }
        private string Execute_Insert_Sql_WithReturn(SqlAction action, SqlConnection connection)
        {
            try
            {
                var return_value = "";
                using (var command = new SqlCommand(action.Sql, connection))
                {
                    if (action.Link.Insert.Columns != null && action.Link.Insert.Columns.Any(i => i != null))
                    {
                        foreach (var column in action.Link.Insert.Columns)
                        {
                            command.Parameters.AddWithValue(
                                column.Column_Name.StartsWith("@")
                                    ? column.Column_Name
                                    : string.Format("@{0}", column.Column_Name),
                                column.Value);
                        }
                    }
                    return_value = command.ExecuteScalar().ToString();
                }
                return return_value;
            }
            catch (System.Exception e)
            {
                return "";
            }
        }
        private bool Execute_Insert_Sql(SqlAction action, SqlConnection connection)
        {
            try
            {
                var return_value = false;
                using (var command = new SqlCommand(action.Sql, connection))
                {
                    if (action.Link.Insert.Columns != null && action.Link.Insert.Columns.Any(i => i != null))
                    {
                        foreach (var column in action.Link.Insert.Columns)
                        {
                            command.Parameters.AddWithValue(
                                column.Column_Name.StartsWith("@")
                                    ? column.Column_Name
                                    : string.Format("@{0}", column.Column_Name),
                                column.Value);
                        }
                    }
                    return_value = command.ExecuteNonQuery() > 0;
                }
                return return_value;
            }
            catch (System.Exception e)
            {
                return false;
            }
        }
        private bool Execute_Update_Sql(SqlAction action, SqlConnection connection)
        {
            try
            {
                var return_value = false;
                using (var command = new SqlCommand(action.Sql, connection))
                {
                    if (action.Link.Insert.Columns != null && action.Link.Insert.Columns.Any(i => i != null))
                    {
                        foreach (var column in action.Link.Insert.Columns)
                        {
                            command.Parameters.AddWithValue(
                                column.Column_Name.StartsWith("@")
                                    ? column.Column_Name
                                    : string.Format("@{0}", column.Column_Name),
                                column.Value);
                        }
                    }
                    return_value = command.ExecuteNonQuery() > 0;
                }
                return return_value;
            }
            catch (System.Exception e)
            {
                return false;
            }
        }
        #endregion

        #region Process extra select action
        private Result Process_File(SqlAction action, DataTable selectReturnResult)
        {
            try
            {
                File.WriteAllBytes(action.Link.Select.File_Path, Convert_To_Excel(action, selectReturnResult));
                return Results.Success("File Saved");
            }
            catch (System.Exception e)
            {
                return e.ToResult();
            }
        }
        private Result Process_Email(SqlAction action, DataTable selectReturnResult)
        {
            try
            {
                var excel = Convert_To_Excel(action, selectReturnResult);
                var setting = action.Link.Select.EmailSetting;
                var email = action.Link.Select.Email;
                using (var stream = new MemoryStream(excel))
                {
                    var to = email.To.ToString().Replace(";", "");
                    var message = new System.Net.Mail.MailMessage(setting.FromAddress, to)
                    {
                        Subject = email.Subject, 
                        Body = email.Body, 
                        IsBodyHtml = false,
                    };
                    if (!string.IsNullOrEmpty(email.Cc))
                    {
                        message.CC.Add(email.Cc.ToString().Replace(";", ""));
                    }
                    message.Attachments.Add(new System.Net.Mail.Attachment(stream, email.FileName + ".zip"));
                    var smtp = new System.Net.Mail.SmtpClient(setting.Smtp_Serber_Ip_Or_Name)
                    {
                        Timeout = 600000,
                        DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                    };
                    smtp.Send(message);
                    return Results.Success("Email Sent");
                }
            }
            catch (System.Exception e)
            {
                return e.ToResult();
            }
        }
        #endregion

        private byte[] Convert_To_Excel(SqlAction action, DataTable selectReturnResult)
        {
            try
            {
                return ExcelHelpers.BuildExcelFile(selectReturnResult, action.Link.Select.Email.FileName);
            }
            catch (System.Exception e)
            {
                var result = e.ToResult();
                return new byte[]{};
            }
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}