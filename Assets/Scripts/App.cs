using QFramework.UIWidgets.ReduxPersist;
using Unity.UIWidgets;
using Unity.UIWidgets.engine;
using Unity.UIWidgets.material;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;
using Color = Unity.UIWidgets.ui.Color;

namespace TodoProApp
{
    public class App : UIWidgetsPanel
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            FontManager.instance.addFont(Resources.Load<Font>("MaterialIcons-Regular"), "Material Icons");
        }

        protected override Widget createWidget()
        {
            AppState initState = null;
            
//            try
//            {
                initState = AppState.Load();
//            }
//            catch (Exception e)
//            {
//                PlayerPrefs.DeleteKey("REDUX_PERISIST");
//                initState = AppState.Load();
//            }

            var store = new Store<AppState>(AppReducer.Reduce,
                initState,
                ReduxPersistMiddleware.create<AppState>());

            return new StoreProvider<AppState>(store, child:
                new MaterialApp(
                    theme:new ThemeData(
                        accentColor:Colors.orange,primaryColor:new Color(0xFFDE4435)
                        ),
                    home: new Home()
                )
            );
        }
    }
}