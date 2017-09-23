using System;

namespace Work_Tracker.Business.Models
{
    public abstract class ObjectBase
    {
        /// <summary>
        /// The _id
        /// </summary>
        private Guid? _id;
        /// <summary>
        /// The _created
        /// </summary>
        private DateTime? _created;
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectBase"/> class.
        /// </summary>
        protected ObjectBase()
        {
            _created = DateTime.Now;
        }
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Guid Id
        {
            get { return (_id ?? (_id = Guid.NewGuid())).Value; }
            set { _id = value; }
        }
        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public DateTime Created
        {
            get { return (_created ?? (_created = DateTime.Now)).Value; }
            set { _created = value; }
        }
    }
}