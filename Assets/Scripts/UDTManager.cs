using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class UDTManager : MonoBehaviour, IUserDefinedTargetEventHandler
{

    UserDefinedTargetBuildingBehaviour udt_targetBuildingBehavior;

    ObjectTracker objectTracker;
    DataSet dataSet;
    int TrackerCounter;

    ImageTargetBuilder.FrameQuality udt_FrameQuality;
    public ImageTargetBehaviour targetBehaviour;

    private void Start()
    {
        targetBehaviour = GetComponent<ImageTargetBehaviour>();
        targetBehaviour.enabled = false;
        udt_targetBuildingBehavior = GetComponent<UserDefinedTargetBuildingBehaviour>(); //get UserDefinedTargetBuilding from current gameObject4
        if (udt_targetBuildingBehavior) //if not null
        {
            udt_targetBuildingBehavior.RegisterEventHandler(this);
        }
    }

    //method updates Frame Quality
    public void OnFrameQualityChanged(ImageTargetBuilder.FrameQuality frameQuality)
    {
        udt_FrameQuality = frameQuality;
        //throw new System.NotImplementedException();
    }

    public void OnInitialized()
    {
        objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
        if (objectTracker != null) //if not null
        {
            dataSet = objectTracker.CreateDataSet(); //creating a new data set
            objectTracker.ActivateDataSet(dataSet);
        }
    }

    public void OnNewTrackableSource(TrackableSource trackableSource)
    {
        TrackerCounter++;
        objectTracker.DeactivateDataSet(dataSet);

        dataSet.CreateTrackable(trackableSource, targetBehaviour.gameObject);

        objectTracker.ActivateDataSet(dataSet);

        udt_targetBuildingBehavior.StartScanning();

        // throw new System.NotImplementedException();
    }

    public void BuildTarget()
    {
        if (udt_FrameQuality == ImageTargetBuilder.FrameQuality.FRAME_QUALITY_HIGH)
        {
            //I want to build a new target
            udt_targetBuildingBehavior.BuildNewTarget(TrackerCounter.ToString(), targetBehaviour.GetSize().x);
        }
    }
}