using System;

namespace FoldersLinkRightClickMenu.Singletons
{
    public sealed class ApplicationSingleton
    {
        #region Singleton

        private ApplicationSingleton() { }
        private static volatile ApplicationSingleton instance;
        private static object syncRoot = new Object();
        private string _linkFolder;

        public static ApplicationSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new ApplicationSingleton();
                    }
                }

                return instance;
            }
        }

        #endregion

        public bool IsLinkFolderValid
        {
            get { return !string.IsNullOrWhiteSpace(Instance._linkFolder); }
            private set { }
        }
        public string LinkFolder
        {
            get { return Instance._linkFolder; }
            set { Instance._linkFolder = value; }
        }
    }
}