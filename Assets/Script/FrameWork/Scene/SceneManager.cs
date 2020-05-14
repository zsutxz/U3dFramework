using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace U3dFramework{

    public class SceneManager : Singleton<SceneManager>{

    #region SceneInfoData class
        public class SceneInfoData{
            public Type SceneType{  get;    private set;  }
            public string SceneName{  get;  private set;  }
            public object[] Params{  get;    private set;  }
            public SceneInfoData(string _sceneName, Type _sceneType, params object[] _params){
                this.SceneType = _sceneType;
                this.SceneName = _sceneName;
                this.Params = _params;
            }
        }
   #endregion
        private Dictionary<EnumSceneType, SceneInfoData> dicSceneInfos = null;
        private BaseScene currentScene = new BaseScene();
        public BaseScene CurrentScene{
            get{
                return currentScene;
            }

            set{
                currentScene = value;
                if(null != currentScene){
                    currentScene.Load();
                }
            }
        }

        public EnumSceneType LastSceneType{  get;    set;  }
        public EnumSceneType ChangeSceneType{  get;    private set;  }
        private EnumUIType sceneOpenUIType = EnumUIType.None;

        private object[] sceneOpenUIParams = null;

      private EnumSceneType ChangeSceneId= EnumSceneType.None;

//       public override void Init(){
       public void Init(){

            dicSceneInfos = new Dictionary<EnumSceneType, SceneInfoData>();

        }

        public void RegisterAllScene(){

            RegisterScene(EnumSceneType.StartGame, "StartGameScene", null, null);

            RegisterScene(EnumSceneType.LoginScene, "LoginScene", typeof(BaseScene), null);

            RegisterScene(EnumSceneType.MainScene, "MainScene", null, null);

            RegisterScene(EnumSceneType.CopyScene, "CopyScene", null, null);

        }

        public void RegisterScene(EnumSceneType _sceneID, string _sceneName, Type _sceneType,params System.Object[] _params){

             if(_sceneType == null || _sceneType.BaseType != typeof(BaseScene)){
                  throw new Exception("Register scene type must not null and extends BaseScene");
             }

             if(dicSceneInfos.ContainsKey(_sceneID)){
               SceneInfoData sceneInfo = new SceneInfoData(_sceneName, _sceneType, _params);
               dicSceneInfos.Add(_sceneID, sceneInfo);
           }
        }

 

        public void UnRegisterScene(EnumSceneType _sceneID){

            if(dicSceneInfos.ContainsKey(_sceneID)){

                dicSceneInfos.Remove(_sceneID);
            }
        }

        public bool IsRegisterScene(EnumSceneType _sceneID){
            return dicSceneInfos.ContainsKey(_sceneID);
        }


        internal BaseScene GetBaseScene(EnumSceneType _sceneType){

            Debug.Log("GetBaseScene sceneId = " + _sceneType.ToString());

            SceneInfoData sceneInfo = GetSceneInfo(_sceneType);

            if(sceneInfo == null || sceneInfo.SceneType == null){

                return null;
            }

            BaseScene scene = System.Activator.CreateInstance(sceneInfo.SceneType) as BaseScene;
            return scene;

            //BaseScene scene = Game.Instance.GetBaseScene(Game.Instance.ChangeSceneId);
            //Game.Instance.CurrentScene = scene;
            //scene.Load();
        }

        public SceneInfoData GetSceneInfo(EnumSceneType _sceneID){

            if(dicSceneInfos.ContainsKey(_sceneID)){
                return dicSceneInfos[_sceneID];
            }

            Debug.LogError("This Scene is not register! ID:" + _sceneID.ToString());

            return null;
        }

 

        public string GetSceneName(EnumSceneType _sceneID){

            if(dicSceneInfos.ContainsKey(_sceneID)){
                return dicSceneInfos[_sceneID].SceneName;
            }

            Debug.LogError("This Scene is not register! ID:" + _sceneID.ToString());

            return null;
        }

        public void ClearScene(){
            dicSceneInfos.Clear();
        }

        #region Change Scene
        public void ChangeSceneDirect(EnumSceneType _sceneType){

            //UIManager.Instance.CloseUIAll();

            if(CurrentScene != null){
                CurrentScene.Release();
                CurrentScene = null;
            }

            LastSceneType = ChangeSceneType;
            ChangeSceneType = _sceneType;

            string sceneName = GetSceneName(_sceneType);

            //change scene
            //CoroutineController.Instance.StartCoroutine(AsyncLoadScene(sceneName));
        }

        public void ChangeSceneDirect(EnumSceneType _sceneType, EnumUIType _uiType, params object[] _params){

            sceneOpenUIType = _uiType;
            sceneOpenUIParams = _params;

            if(LastSceneType == _sceneType){
                if(sceneOpenUIType == EnumUIType.None){
                    return;
                }

               //UIManager.Instance.OpenUI(sceneOpenUIType, sceneOpenUIParams);
                sceneOpenUIType = EnumUIType.None;

            }else{
                ChangeSceneDirect(_sceneType);
            }
        }

        private IEnumerator<AsyncOperation> AsyncLoadScene(string sceneName){

            AsyncOperation oper = Application.LoadLevelAsync(sceneName);

            yield return oper;

//              //message send
//             if(sceneOpenUIType != EnumType.None){
//                 UIManager.Instance.OpenUI(sceneOpenUIType, sceneOpenUIParams);
//                 sceneOpenUIType = EnumUIType.None;
//             }
        }

        #endregion

 
        public void ChangeScene(EnumSceneType _sceneType){

            //UIManager.Instance.CloseUIAll();

            if(CurrentScene != null){
                CurrentScene.Release();
                CurrentScene = null;
            }

            LastSceneType = ChangeSceneType;
            ChangeSceneType = _sceneType;

            string sceneName = GetSceneName(_sceneType);

            //change loading scene
            //CoroutineController.Instance.StartCoroutine(AsyncLoadOtherScene());
        }

        public void ChangeScene(EnumSceneType _sceneType, EnumUIType _uiType, params object[] _params){

            sceneOpenUIType = _uiType;
            sceneOpenUIParams = _params;

            if(LastSceneType == _sceneType){
                if(sceneOpenUIType == EnumUIType.None){
                    return;
                }

               //UIManager.Instance.OpenUI(sceneOpenUIType, sceneOpenUIParams);
               sceneOpenUIType = EnumUIType.None;
            }
            else
            {
                ChangeScene(_sceneType);
            }
        }

        private IEnumerator AsyncLoadOtherScene(){

            string sceneName = GetSceneName(EnumSceneType.LoadingScene);
            AsyncOperation oper = Application.LoadLevelAsync(sceneName);
            yield return oper;

            //message send
            if(oper.isDone){

//                 GameObject go = GameObject.Find("LoadingScenePanel");
//                 LoadingSceneUI loadingSceneUI = go.GetComponent<LoadingSceneUI>();
//                 BaseScene scene = CurrentScene;
//                 if(null != scene){
//                     scene.CurrentSceneId = ChangeSceneId;
//                 }

                //检测是否注册该场景
                if(!SceneManager.Instance.IsRegisterScene(ChangeSceneId)){
                    Debug.LogError("没有注册此场景：" + ChangeSceneId.ToString());
                }

//                 LoadingSceneUI.Load(ChangeSceneId);
//                 LoadingSceneUI.LoadCompleted += SceneLoadCompleted;
            }    
        }

        void SceneLoadCompleted(object sender, EventArgs e){

            Debug.Log("切换场景完成 +" + sender as String);

            //场景切换完成
            //MessageCenter.Instance.SendMessage(MessageType.GAMESCENE_CHANGECOMPLETE, this, null, false);

             //有要打开的UI
            if(sceneOpenUIType != EnumUIType.None){
                //UIManager.Instance.OpenUI(false, sceneOpenUIType, sceneOpenUIParams);
                sceneOpenUIType = EnumUIType.None;
            }
        }

        //加载场景
        private IEnumerator AsyncLoadedNextScene(){

            string fileName = SceneManager.Instance.GetSceneName(ChangeSceneId);
            AsyncOperation oper = Application.LoadLevelAsync(fileName);

            yield return oper;

            if(oper.isDone){

//                 if(LoadCompleted != null){
//                     LoadCompleted(changeSceneId, null);
//                 }
            }
        }
    }

}
