using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExampleArmy : MonoBehaviour {
    private FormationBase _formation;

    public FormationBase Formation {
        get {
            if (_formation == null) _formation = GetComponent<FormationBase>();
            return _formation;
        }
        set => _formation = value;
    }
    

    [SerializeField] private SimpleAi _unitPrefab;
    [SerializeField] private float _unitSpeed = 2;
    [SerializeField] List<FormationBase> Formations;
    int index = 0;

    private readonly List<IPositonable > _spawnedUnits = new List<IPositonable>();
    private List<Vector3> _points  = new List<Vector3>();
    private Transform _parent;
   

    private void Awake() {
        _parent = new GameObject("Unit Parent").transform;
        _parent.transform.parent = transform;
       
         SetFormation();
       
    }

    private void Update() {
       SetFormation();
       if(Input.GetKeyDown(KeyCode.Space))
         {
             index +=1;
            if(index == Formations.Count)
            index = 0;
            _formation = Formations[index];
         }   
    }

    private void SetFormation() {
        _points = Formation.EvaluatePoints().ToList();

        if (_points.Count > _spawnedUnits.Count) {
            var remainingPoints = _points.Skip(_spawnedUnits.Count);
            Spawn(remainingPoints);
        }
        else if (_points.Count < _spawnedUnits.Count) {
            Kill(_spawnedUnits.Count - _points.Count);
        }

        for (var i = 0; i < _spawnedUnits.Count; i++) {
            
            _spawnedUnits[i].SetPositon(_points[i]); 
        }
    }

    private void Spawn(IEnumerable<Vector3> points) {
        foreach (var pos in points) {
            var unit = Instantiate(_unitPrefab, transform.position + pos, Quaternion.identity, _parent);
            _spawnedUnits.Add(unit);
        }
    }

    private void Kill(int num) {
        for (var i = 0; i < num; i++) {
            var unit = _spawnedUnits.Last();
            _spawnedUnits.Remove(unit);
            unit.Disable();
        }
    }
}