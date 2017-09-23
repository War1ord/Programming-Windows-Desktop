namespace UI.Models
{
    public class DialogResult<T>
    {
        public bool Success { get; set; }
        public T Result { get; set; }
    }
}