using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
    public class EventManager : MonoBehaviour
    {
        public CarController playerCar;
        float time_since_last_update;
        // Start is called before the first frame update
        void Start()
        {
            playerCar = gameObject.GetComponent<CarController>();
            time_since_last_update = 0.0f;
        }

        // Update is called once per frame
        void Update()
        {
            time_since_last_update += Time.deltaTime;

            if(time_since_last_update >= 0.1f)
            {
                Debug.Log(playerCar.transform.position.x.ToString());                
                time_since_last_update = 0.0f;

            }
            
        }
    }
}
