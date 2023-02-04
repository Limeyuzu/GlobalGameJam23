using UnityEngine;

public class WeedRenderer : MonoBehaviour
{
    private GameObject _weedModel1;
    private GameObject _weedModel2;
    private GameObject _weedModel3;
    private GardenTile _parentTile;

    // Start is called before the first frame update
    void Start()
    {
        _parentTile = GetComponent<GardenTile>();
        _weedModel1 = transform.Find("WeedSmall").gameObject;
        _weedModel2 = transform.Find("WeedMedium").gameObject;
        _weedModel3 = transform.Find("WeedLarge").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        var growth = _parentTile.GetGrowthValue();
        if (growth < 1)
        {
            _weedModel1.SetActive(false);
            _weedModel2.SetActive(false);
            _weedModel3.SetActive(false);
        }
        else if (growth >= 1 && growth < 2)
        {
            _weedModel1.SetActive(true);
        }
        else if (growth >= 2 && growth < 3)
        {
            _weedModel2.SetActive(true);
        }
        else if (growth >= 3)
        {
            _weedModel3.SetActive(true);
        }
    }
}
