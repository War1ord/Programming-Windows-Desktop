namespace Sql_Business_Functions.Business.Models
{
    public enum FilterType
    {
        Equals                     , // = (Equals)
        Greater_Than               , // > (Greater Than)
        Less_Than                  , // < (Less Than)
        Greater_Than_Or_Equal_To   , // >= (Greater Than or Equal To)
        Less_Than_Or_Equal_To      , // <= (Less Than or Equal To)
        Not_Equal_To               , // <> (Not Equal To)
        Not_Less_Than              , // !< (Not Less Than) (not ISO standard)
        Not_Greater_Than           , // !> (Not Greater Than) (not ISO standard)
        Like                       , // LIKE '%' + @value + '%' NOTE: not yet implemented
        Not_Like                   , // NOT LIKE '%' + @value + '%' NOTE: not yet implemented
        Like_Right                 , // LIKE @value + '%' NOTE: not yet implemented
        Not_Like_Right             , // NOT LIKE @value + '%' NOTE: not yet implemented
        Like_Left                  , // LIKE '%' + @value NOTE: not yet implemented
        Not_Like_Left              , // NOT LIKE '%' + @value NOTE: not yet implemented
        Is_Null                    , // IS NULL NOTE: not yet implemented
        Is_Not_Null                , // IS NOT NULL NOTE: not yet implemented
        In                         , // IN (@values)  NOTE: not yet implemented
        Not_In                     , // NOT IN (@values) NOTE: not yet implemented
    }
}