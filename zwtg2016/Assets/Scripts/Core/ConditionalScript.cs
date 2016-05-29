using UnityEngine;
using System.Collections;
using System;

public class ConditionalScript : MonoBehaviour {

	public PunTeams.Team team;

	public UnityEngine.Object[] scripts;


	void addComponentScripts () {

		foreach (UnityEngine.Object script in scripts) {
			gameObject.AddComponent (Type.GetType (script.name));
		}
	}

	public void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
	{
		PhotonPlayer player = playerAndUpdatedProps[0] as PhotonPlayer;

		if (player == PhotonNetwork.player) {
			if(player.GetTeam() == team) {
			
				addComponentScripts();
			}
		}
	}
}
