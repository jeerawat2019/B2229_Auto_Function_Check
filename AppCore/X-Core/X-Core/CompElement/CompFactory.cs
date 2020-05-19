using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace X_Core.CompElement
{
    public class CompFactory
    {


        #region Privates

        private CompBase _comp = null;
        private string _name = string.Empty;
        private string _nickname = string.Empty;
        private Type _tyCompClass = null;
        private string _pluginName = string.Empty;
        private Type _pluginBaseType = null;
        private List<CompFactory> _children = new List<CompFactory>();
        private object[] _initParms = null;

        #endregion Privates

        #region Public Properties

        /// <summary>
        /// Get the Nickname for this component
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Get the PluginName for this component
        /// </summary>
        public string PluginName
        {
            get { return _pluginName; }
        }

        /// <summary>
        /// Get the Type for this component
        /// </summary>
        public Type CompType
        {
            get { return _tyCompClass; }
        }
        /// <summary>
        /// Get the Plugin Base type for this component
        /// </summary>
        public Type PluginBaseType
        {
            get { return _pluginBaseType; }
        }
        /// <summary>
        /// Get the Initialize parms
        /// </summary>
        public object[] InitParms
        {
            get { return _initParms; }
        }
        #endregion Public Properties

        #region Contructors

        /// <summary>
        /// Constructor for Component
        /// </summary>
        public CompFactory(CompBase comp)
        {
            _comp = comp;
            _name = comp.Name;
        }

        /// <summary>
        /// Constructor for Component
        /// </summary>
        public CompFactory(Type tyComp, string name) :
            this(tyComp, new object[] { name } )
        {

        }

        /// <summary>
        /// Constructor for Component
        /// </summary>
        public CompFactory(Type tyComp, object[] initParms)
        {
            _tyCompClass = tyComp;
            _name = initParms[0] as string;
            _initParms = initParms;
        }

        /// <summary>
        /// Constructor for Plugin
        /// </summary>
        public CompFactory(Enum plugin, params object[] initParms)
        {
            Type ty = plugin.GetType();
            _pluginBaseType = ty.DeclaringType;
            _pluginName = plugin.ToString();
            _name = initParms[0] as string;
            _initParms = initParms;
        }

        #endregion Contructors

        public CompFactory Add(Enum plugin, params object[] initParms)
        {
            CompFactory compDef = new CompFactory(plugin, initParms);
            _children.Add(compDef);
            return compDef;
        }
        public CompFactory Add(Type tyComp)
        {
            CompFactory compDef = new CompFactory(tyComp, string.Empty);
            _children.Add(compDef);
            return compDef;
        }
        public CompFactory Add(Type tyComp, string name)
        {
            CompFactory compDef = new CompFactory(tyComp, name);
            _children.Add(compDef);
            return compDef;
        }
        public CompFactory Add(Type tyComp, params object[] initParms)
        {
            CompFactory compDef = new CompFactory(tyComp, initParms);
            _children.Add(compDef);
            return compDef;
        }

        public CompFactory Add(CompBase comp)
        {
            CompFactory compDef = new CompFactory(comp);
            _children.Add(compDef);
            return compDef;
        }


        public void GetUnloadedPlugins(List<CompFactory> unresolvedPlugins)
        {
            if (_tyCompClass == null && !string.IsNullOrEmpty(_pluginName))
            {
                if (!unresolvedPlugins.Exists(c => c._pluginName == _pluginName))
                {
                    unresolvedPlugins.Add(this);
                }
            }
            foreach (CompFactory compDef in _children)
            {
                compDef.GetUnloadedPlugins(unresolvedPlugins);
            }
        }

        /// <summary>
        /// Confirms that we do not have any plugins definitions that are defined in any loaded non-plugin
        /// </summary>
        /// <param name="tyLoaded"></param>
        /// <returns>Returns true when found and replaced</returns>
        public bool ConfirmNotPluginType(Type tyLoaded)
        {
            if (_pluginName == tyLoaded.Name)
            {
                X_CoreS.LogPopup("The type '{0}' should not be referenced to the main application.  Please load using the PlugIn folder.", tyLoaded.Name);
                return true;
            }
            foreach (CompFactory compDef in _children)
            {
                if (compDef.ConfirmNotPluginType(tyLoaded))
                    return true;
            }
            return false;
        }
        
        /// <summary>
        /// Looks for and assignes the plugin type
        /// </summary>
        /// <param name="tyPlugIn"></param>
        /// <returns>Returns true when found and replaced</returns>
        public void AssignPlugInType(Type tyPlugIn)
        {
            if (_pluginName == tyPlugIn.Name)
            {
                _tyCompClass = tyPlugIn;
            }
            foreach (CompFactory compDef in _children)
            {
                compDef.AssignPlugInType(tyPlugIn);
            }
        }

        /// <summary>
        /// Looks for and assignes the plugin type
        /// </summary>
        /// <param name="listCreatedClasses"></param>
        /// <returns>Returns true when found and replaced</returns>
        public void AssignUnresolvedPlugin(Type[] listCreatedClasses)
        {
            if (_tyCompClass == null && _comp == null)
            {
                try
                {
                    _tyCompClass = listCreatedClasses.Where(c => c.Name == _pluginName).Single();
                }
                catch(Exception ex)
                {
                    X_CoreS.LogPopup(ex, "Could not find the type {0}", _pluginName);
                }
            }
            foreach (CompFactory compDef in _children)
            {
                compDef.AssignUnresolvedPlugin(listCreatedClasses);
            }
        }

        private void CreateComponent(CompBase compParent)
        {
            // Parent will exist
            foreach (CompFactory compChildDef in _children)
            {
                CompBase child = null;
                if (compChildDef._comp != null)
                {
                    child = compParent.FindChild(c => string.Equals(c.Name, compChildDef._comp.Name));
                    if (child == null)
                    {
                        child = compChildDef._comp;
                        compParent.Add(child);
                        //U.LogInfo("Added child {0}", child.Name);
                    }
                    else
                    {
                        child.Name = compChildDef._comp.Name;
                    }
                }
                else
                {
                    // Does the child exist?
                    child = compParent.FindChild(c => c.Name == compChildDef.Name);
                    if (child == null)
                    {
                        // Need to add it
                        if (compChildDef.CompType == null)
                        {
                            throw new X_CoreExceptionPopup("Missing Type for {0}", compChildDef.Name);
                        }
                        if (compChildDef._initParms == null || compChildDef._initParms.Length == 0)
                        {
                            throw new X_CoreExceptionPopup("Missing name or initilaizer {0}", compChildDef.Name);
                        }
                        try
                        {
                            // Create it 
                            child = Activator.CreateInstance(compChildDef.CompType, compChildDef._initParms) as CompBase;
                            compParent.Add(child);
                        }
                        catch (Exception ex)
                        {
                            throw new X_CoreExceptionPopup(ex, "Problem loading dll for {0}", compChildDef.CompType.Name);
                        }
                    }
                    else
                    {
                        child.Name = compChildDef.Name;
                    }
                }
                compChildDef.CreateComponent(child);
            }
        }

        /// <summary>
        /// Create the components in case they do not exist
        /// </summary>
        /// <param name="comp"></param>
        public void CreateComponents(CompBase comp)
        {
            CreateComponent(comp);
        }
    }

}
