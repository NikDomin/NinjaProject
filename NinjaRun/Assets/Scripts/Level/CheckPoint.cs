using Agent;
using Agent.Player.PlayerStateMachine;
using Input.Old_Input.Types;
using Input.Old_Input;
using Movement;
using UnityEngine;

namespace Level
{
    public class CheckPoint : MonoBehaviour
    {
        public void SpawnHero(GameObject player)
        {
            // GameObject player = FindObjectOfType<PlayerState>(true).gameObject;
            player.transform.position = transform.position;
            player.gameObject.SetActive(true);
            player.GetComponent<PlayerHealth>().SetInvulnerability(2f);
            // NewInputManager.PlayerInput.SwitchCurrentActionMap("Touch");
            OldInputManager.Instance.ChangeActionMap(ActionMaps.Touch);
            player.GetComponent<NewSwipeDetection>().ResetAllValue();
            var playerState = player.GetComponent<PlayerState>();
            playerState.StateMachine.ChangeState(playerState.FlyState);

            var deathWallNewPositionX = transform.position.x - 20;
            DeathWall.deathWall.transform.position =
                new Vector3(deathWallNewPositionX, DeathWall.deathWall.transform.position.y);
            DeathWall.deathWall.CanDisableLevelParts = true;
            
            if(PlayerEndlessRunScore.Instance !=null)
                PlayerEndlessRunScore.Instance.ContinueUpdateScore();
        }
    }
}