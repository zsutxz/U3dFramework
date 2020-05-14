using System.Collections;
using System.Collections.Generic;
using System;

namespace U3dFramework{

    public class ModuleManager : Singleton<ModuleManager>{

        private Dictionary<string, BaseModule> dicModules = null;

        public void Init(){
            dicModules = new Dictionary<string, BaseModule>();
        }

        #region Get Module
        public BaseModule Get(string key){
            if(dicModules.ContainsKey(key)){
                return dicModules[key];
            }
            return null;
        }

         public T Get<T>() where T : BaseModule{

            Type t = typeof(T);

            //return Get(t.ToString()) as T;
            if(dicModules.ContainsKey(t.ToString())){
                return dicModules[t.ToString()] as T;
            }

            return null;
        }
        #endregion

        public void RegisterAllModules(){
            //LoadModule(typeof(TestOneModule));
            //LoadModule(typeof(MailModule));    //立钻哥哥：新增邮件模块
            // ..... add
        }

        private void LoadModule(Type moduleType){

            BaseModule bm = System.Activator.CreateInstance(moduleType) as BaseModule;
            bm.Load();
        }

        #region Register Module By Module Type

        public void Register(BaseModule module){

            Type t = module.GetType();
            Register(t.ToString(), module);
        }

        public void Register(string key, BaseModule module){

            if(!dicModules.ContainsKey(key)){
                dicModules.Add(key, module);
            }
        }
        #endregion

        #region Un Register Module
        public void UnRegister(BaseModule module){

            Type t = module.GetType();
            UnRegister(t.ToString());
        }

        public void UnRegister(string key){

            if(dicModules.ContainsKey(key)){

                BaseModule module = dicModules[key];
                module.Release();
                dicModules.Remove(key);
                module = null;
            }
        }

        public void UnRegisterAll(){

            List<string> _keyList = new List<string>(dicModules.Keys);
            for(int i = 0; i < _keyList.Count; i++){
                UnRegister(_keyList[i]);
            }
            dicModules.Clear();
        }

        #endregion
    }
}