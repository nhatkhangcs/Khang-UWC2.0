using Mapbox.Map;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class ResourceManager : PersistentSingleton<ResourceManager>
{
    // @formatter:off
    
    [Header("Map Entities")]
    public JanitorMapEntity JanitorMapEntity;
    public CollectorMapEntity CollectorMapEntity;
    public MCPMapEntity MCPMapEntity;
    public RoutePolyline RoutePolyline;
    
    [Header("Map UI")]
    public AssigningMCPListItemView AssigningMcpListItemView;
    
    [Header("Item Views")] 
    public StaffDataListItemView StaffDataListItemView;
    public MCPDataListItemView MCPDataListItemView;
    public VehicleDataListItemView VehicleDataListItemView;
    public InboxListItemView InboxListItemView;
    public MessageDataListItemView MessageDataListItemView;
    public TaskDataListItemView TaskDataListItemView;

    [Header("Settings")]
    public SettingSectionHeader SettingSectionHeader;
    public GameObject SettingListItemView;
    public Button SettingOptionButton;
    
    public Notification Notification;
    
    // @formatter:on
}