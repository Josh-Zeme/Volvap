using System.Collections.Generic;
using UnityEngine;

public class Bowl : MonoBehaviour
{
    [SerializeField] private Treat _BaseTreat;
    [SerializeField] private Transform _TreatDropZone;
    
    private List<Treat> _Treats = new List<Treat>();

    public int TreatCount => _Treats.Count;

    private void Start()
    {

    }

    public void AddTreat()
    {
        var _treat = Instantiate(_BaseTreat, transform);
        _treat.transform.position = _TreatDropZone.position;
        _Treats.Add(_treat);
    }

    public void DestroyTreats()
    {
        for(int _i = 0; _i < _Treats.Count; ++_i)
        {
            var _treat = _Treats[_i];
            Destroy(_treat.gameObject);
        }
        _Treats.Clear();
    }
}
