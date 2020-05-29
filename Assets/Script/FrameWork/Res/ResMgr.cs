using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace U3dFramework
{
    //资源信息
    public class AssetInfo
    {
        private UnityEngine.Object _Object;

        public Type AssetType { get; set; }
        public string Path { get; set; }
        public int RefCount { get; set; }

        public bool IsLoaded
        {
            get
            {
                return null != _Object;
            }
        }

        public UnityEngine.Object AssetObject
        {
            get
            {
                if (null == _Object)
                {
                    //_Object = Resources.Load(Path);
                    _ResourcesLoad();
                }
                return _Object;
            }
        }

        public IEnumerator GetCoroutineObject(Action<UnityEngine.Object> _loaded)
        {
            while (true)
            {
                yield return null;

                if (null == _Object)
                {
                    //yield return null;
                    //_Object = Resources.Load(Path);

                    _ResourcesLoad();

                    yield return null;
                }

                if (null != _loaded)
                {
                    _loaded(_Object);

                }

                yield break;

            }

        }

        private void _ResourcesLoad()
        {
            try
            {
                _Object = Resources.Load(Path);

                if (null == _Object)
                {
                    Debug.Log("Resources Load Failure! Path: " + Path);

                }

            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }
        }

        public IEnumerator GetAsyncObject(Action<UnityEngine.Object> _loaded)
        {
            return GetAsyncObject(_loaded, null);
        }

        public IEnumerator GetAsyncObject(Action<UnityEngine.Object> _loaded, Action<float> _progress)
        {
            //have Object
            if (null != _Object)
            {
                _loaded(_Object);
                yield break;
            }

            //Object null. Not Load Resources
            ResourceRequest _resRequest = Resources.LoadAsync(Path);

            while (_resRequest.progress < 0.9)
            {
                if (null != _progress)
                {
                    _progress(_resRequest.progress);
                }

                yield return null;
            }

            while (!_resRequest.isDone)
            {
                if (null != _progress)
                {
                    _progress(_resRequest.progress);
                }

                yield return null;
            }

            ///???
            _Object = _resRequest.asset;
            if (null != _loaded)
            {
                _loaded(_Object);
            }

            yield return _resRequest;
        }
    }

    //ResManger：资源管理器
    public class ResManager : MonoSingleton<ResManager>
    {
        private Dictionary<string, AssetInfo> dicAssetInfo = null;

        public void Init()
        {
            //Resources.Load();
            //Resources.LoadAsync();
            //Resources.LoadAll();
            //Resources.LoadAssetAtPath();

            dicAssetInfo = new Dictionary<string, AssetInfo>();

            Debug.Log("ResManager: Singleton < ResManager > Init");
        }

        #region Load Resource & Instantiate Object
        public UnityEngine.Object LoadInstance(string _path)
        {
            UnityEngine.Object _obj = Load(_path);

            //MonoBehaviour.Instantiate();
            return Instantiate(_obj);
        }
        #endregion

        private UnityEngine.Object Instantiate(UnityEngine.Object _obj)
        {
            return Instantiate(_obj, null);
        }

        private UnityEngine.Object Instantiate(UnityEngine.Object _obj, Action<UnityEngine.Object> _loaded)
        {
            UnityEngine.Object _retObj = null;

            if (null != _obj)
            {
                _retObj = MonoBehaviour.Instantiate(_obj);

                if (null != _retObj)
                {
                    if (null != _loaded)
                    {
                        _loaded(_retObj);
                    }
                    else
                    {
                        Debug.LogError("null _loaded.");
                    }

                    return _retObj;
                }
                else
                {
                    Debug.LogError("null Instantiate _retObj.");
                }
            }
            else
            {
                Debug.LogError("Error：null Resources Load return _obj.");
            }
            return null;
        }

        #region Load Resource
        public UnityEngine.Object Load(string _path)
        {
            AssetInfo _assetInfo = GetAssetInfo(_path);

            if (null != _assetInfo)
            {
                return _assetInfo.AssetObject;
            }
            return null;
        }
        #endregion

        #region Load Coroutine Resources
        public void LoadCoroutine(string _path, Action<UnityEngine.Object> _loaded)
        {
            AssetInfo _assetInfo = GetAssetInfo(_path, _loaded);

            if (null != _assetInfo)
            {
                StartCoroutine(_assetInfo.GetCoroutineObject(_loaded));
            }
        }
        #endregion

        #region Load Async Resources
        public void LoadAsync(string _path, Action<UnityEngine.Object> _loaded)
        {
            LoadAsync(_path, _loaded, null);
        }

        public void LoadAsync(string _path, Action<UnityEngine.Object> _loaded, Action<float> _progress)
        {
            AssetInfo _assetInfo = GetAssetInfo(_path, _loaded);

            if (null != _assetInfo)
            {
                StartCoroutine(_assetInfo.GetAsyncObject(_loaded, _progress));
            }
        }
        #endregion

        private AssetInfo GetAssetInfo(string _path)
        {
            return GetAssetInfo(_path, null);
        }

        private AssetInfo GetAssetInfo(string _path, Action<UnityEngine.Object> _loaded)
        {
            if (string.IsNullOrEmpty(_path))
            {
                Debug.LogError("Error: null _path name.");

                if (null != _loaded)
                {
                    _loaded(null);
                }
            }

            // Load Res ....
            AssetInfo _assetInfo = null;

            if (!dicAssetInfo.TryGetValue(_path, out _assetInfo))
            {
                _assetInfo = new AssetInfo();
                _assetInfo.Path = _path;

                dicAssetInfo.Add(_path, _assetInfo);
            }
            _assetInfo.RefCount++;

            return _assetInfo;
        }
    }
}
