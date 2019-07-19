using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class SimpleCloudHandler : MonoBehaviour, ICloudRecoEventHandler
{

    public ImageTargetBehaviour imageTargetTemplate;
    private CloudRecoBehaviour mcloudRecoBrhaviour;
    private bool mIsScanning = false;
    // Start is called before the first frame update
    void Start()
    {
        CloudRecoBehaviour cloudRecoBehaviour = GetComponent<CloudRecoBehaviour>();
        if(cloudRecoBehaviour){
          cloudRecoBehaviour.RegisterEventHandler(this);
        }
        mcloudRecoBrhaviour = cloudRecoBehaviour;
    }

    public void OnInitialized(TargetFinder finder){

    }

    public void OnInitError(TargetFinder.InitState initError){

    }


    public void OnUpdateError(TargetFinder.UpdateState updateError){

    }

    public void OnStateChanged(bool scanning){
     mIsScanning = scanning;
     if(scanning){
        ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
        tracker.TargetFinder.ClearTrackables(false);
      }
    }

    public void OnNewSearchResult(TargetFinder.TargetSearchResult targetSearchResult){
        GameObject newImageTarget = Instantiate(imageTargetTemplate.gameObject) as GameObject;
        GameObject augmentation = null;
        if(augmentation != null){
            augmentation.transform.SetParent(newImageTarget.transform);
        }
        if(imageTargetTemplate){
        ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
        ImageTargetBehaviour imageTargetBehaviour = (ImageTargetBehaviour) tracker.TargetFinder.EnableTracking(targetSearchResult, newImageTarget);
        }

        if(!mIsScanning){
            mcloudRecoBrhaviour.CloudRecoEnabled = true;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
