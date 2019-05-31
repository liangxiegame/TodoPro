using Newtonsoft.Json;
using UnityEngine;

namespace QFramework.UIWidgets.ReduxPersist
{
    public abstract class AbstractPersistState<T> where T : AbstractPersistState<T>, new()
    {
        private const string KEY = "REDUX_PERISIST";

        public virtual void OnLoaded()
        {
            
        }
        
        public static T Load()
        {
            var jsonContent = PlayerPrefs.GetString(KEY);

            T retModel = null;
            if (string.IsNullOrWhiteSpace(jsonContent))
            {
                retModel = new T();
            }
            else
            {
                retModel = JsonConvert.DeserializeObject<T>(jsonContent);
            }
            
            retModel.OnLoaded();

            return retModel;
        }

        public void Save()
        {
            PlayerPrefs.SetString(KEY, JsonConvert.SerializeObject(this));
        }
    }
}