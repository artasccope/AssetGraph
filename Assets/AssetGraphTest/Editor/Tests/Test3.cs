using UnityEngine;
using UnityEditor;

using AssetGraph;

using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using MiniJSONForAssetGraph;

// 同じFilterの結果を複数のノードが使用する場合、キャッシュが効くかどうか

public partial class Test {
	public void _3_0_OrderWithCache0 () {
		// 根っこあたりにフィルタがあり、4つ又のimportの結果が再度読まれないかどうか
		var basePath = Path.Combine(Application.dataPath, "AssetGraphTest/Editor/TestData");
		var graphDataPath = Path.Combine(basePath, "_3_0_OrderWithCache0.json");
		
		// load
		var dataStr = string.Empty;
		
		using (var sr = new StreamReader(graphDataPath)) {
			dataStr = sr.ReadToEnd();
		}
		var graphDict = Json.Deserialize(dataStr) as Dictionary<string, object>;
		
		var EndpointNodeIdsAndNodeDatasAndConnectionDatas = GraphStackController.SerializeNodeRoute(graphDict);
		
		var endpointNodeIds = EndpointNodeIdsAndNodeDatasAndConnectionDatas.endpointNodeIds;
		var nodeDatas = EndpointNodeIdsAndNodeDatasAndConnectionDatas.nodeDatas;
		var connectionDatas = EndpointNodeIdsAndNodeDatasAndConnectionDatas.connectionDatas;

		var resultDict = new Dictionary<string, Dictionary<string, List<InternalAssetData>>>();

		foreach (var endNodeId in endpointNodeIds) {
			GraphStackController.RunSerializedRoute(endNodeId, nodeDatas, connectionDatas, resultDict);
		}

		// resultDictのチェックをすればよさげなのだが、記録が残しにくい、、
		Debug.LogError("今ならできるな");
	}
}