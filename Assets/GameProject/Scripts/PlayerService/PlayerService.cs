﻿using LevelSystem;
using UnityEngine;
using Common;

namespace PlayerSystem
{
    public class PlayerService : IPlayerService
    {
        private PlayerController playerController;
        private PlayerView playerPrefab;
        private BombController bombPrefab;
        private ILevelService levelService;
        private ServiceManager serviceManager;

        public PlayerService(PlayerView playerPrefab, BombController bombPrefab, ServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
            this.playerPrefab = playerPrefab;
            this.bombPrefab = bombPrefab;
            serviceManager.restartGame += RestartGame;
        }

        ~PlayerService()
        {
            serviceManager.restartGame -= RestartGame;
        }

        public void SpawnPlayer(Vector2 spawnPos)
        {
            playerController = new PlayerController(playerPrefab, bombPrefab.gameObject, spawnPos, this,
            levelService);
        }

        public void PlayerKilled()
        {
            //TODO: fire game lost event
            serviceManager.SetGameStatus(false);
            playerController = null; 
        }

        void RestartGame()
        {
            if (playerController != null)
            {
                playerController.PlayerDestroy();
                playerController = null;
            }
        }

        public GameObject GetPlayer()
        {
            return playerController.GetPlayerView.gameObject;
        }

        public void SetLevelService(ILevelService levelService)
        {
            this.levelService = levelService;
        }
    }
}