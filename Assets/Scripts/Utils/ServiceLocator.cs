using System;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class ServiceLocator : MonoBehaviour
    {
        public static Action<ServiceMessage> serviceAction;
        public static ServiceLocator Instance;
        Dictionary<Type, MonoBehaviour> serviceDict;


        // Start is called before the first frame update
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            serviceDict = new Dictionary<Type, MonoBehaviour>();
            Application.targetFrameRate = 60;
        }

        public T GetService<T>() where T : MonoBehaviour
        {
            if (serviceDict == null)
            {
                return null;
            }

            if (serviceDict.ContainsKey(typeof(T)))
            {
                return (T)serviceDict[typeof(T)];
            }
            else
            {
                T component = GetComponentInChildren<T>();
                if (!component) { return null; }
                serviceDict.Add(typeof(T), component);
                return component;
            }
        }

    }
}
