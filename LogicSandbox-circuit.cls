{
  "components": [
    {
      "id": "c_power_1680",
      "type": "maxstupo.logicsandbox.logic.components.power",
      "x": 7.71908569,
      "y": 40.55669,
      "label": "PWR"
    },
    {
      "id": "c_toggle_1721",
      "type": "maxstupo.logicsandbox.logic.components.toggle",
      "x": 69.15817,
      "y": 14.8247414,
      "label": "Toggle",
      "state": false
    },
    {
      "id": "c_toggle_0710",
      "type": "maxstupo.logicsandbox.logic.components.toggle",
      "x": 69.15817,
      "y": 66.37113,
      "label": "Toggle",
      "state": false
    },
    {
      "id": "c_notgate_3501",
      "type": "maxstupo.logicsandbox.logic.components.notgate",
      "x": 129.98291,
      "y": 14.8247414,
      "label": "NOT"
    },
    {
      "id": "c_notgate_1155",
      "type": "maxstupo.logicsandbox.logic.components.notgate",
      "x": 129.98291,
      "y": 66.37113,
      "label": "NOT"
    },
    {
      "id": "c_orgate_1782",
      "type": "maxstupo.logicsandbox.logic.components.orgate",
      "x": 183.591156,
      "y": 41.1134,
      "label": "OR"
    },
    {
      "id": "c_notgate_0860",
      "type": "maxstupo.logicsandbox.logic.components.notgate",
      "x": 236.168472,
      "y": 41.1134,
      "label": "NOT"
    }
  ],
  "wires": [
    ["c_toggle_1721.in0","c_power_1680.out0"],
    ["c_power_1680.out0","c_toggle_0710.in0"],
    ["c_toggle_0710.out0","c_notgate_1155.in0"],
    ["c_notgate_3501.in0","c_toggle_1721.out0"],
    ["c_orgate_1782.in0","c_notgate_3501.out0"],
    ["c_orgate_1782.in1","c_notgate_1155.out0"],
    ["c_orgate_1782.out0","c_notgate_0860.in0"]
  ]
}