using System;
using System.Collections;
using System.Collections.Generic;
using Models;
using UnityEngine;

public class SceneVars : MonoBehaviour
{
    //public List<Wave> waves;
    public List<Wave> MainMenuWaves;
    public List<Wave> Level1;
    public List<Wave> Level2;
    public List<Wave> Level3;
    public List<Wave> Level4;
    // public List<Wave> Level5;
    
    

    public static SceneVars current;
    
    void Awake()
    {
        #region Main Menu waves

        var wavesMain = new List<Wave>();
        var oneWave = new Wave(Enemy.Add(
            Enemies.Light * 10,
            Enemies.Armed * 10
        ).Shuffle(), TimeSpan.FromSeconds(20));
        wavesMain.Add(oneWave);
        wavesMain.Add(oneWave);
        wavesMain.Add(oneWave);
        wavesMain.Add(oneWave);
        wavesMain.Add(oneWave);

        MainMenuWaves = wavesMain;
        
        #endregion
        
        #region Level 1
        var waves = new List<Wave>();
        waves.Add(new Wave( // wave 1
            new[]
            {
                Enemies.Light, Enemies.Light, Enemies.Light,
            }, TimeSpan.FromSeconds(30)
        ));
        waves.Add(new Wave( //wave 2
            new[]
            {
                Enemies.Light, Enemies.Light, Enemies.Light,
                Enemies.Light, Enemies.Light, Enemies.Light,
            }, TimeSpan.FromSeconds(30)
        ));
        waves.Add(new Wave( //wave 3
            new[]
            {
                Enemies.Light, Enemies.Light, Enemies.Light, Enemies.Armed, Enemies.Armed, 
                Enemies.Light, Enemies.Light, Enemies.Light, Enemies.Armed, Enemies.Armed, 
            }, TimeSpan.FromSeconds(30)
        ));
        waves.Add(new Wave( //wave 4
            new[]
            {
                Enemies.Light, Enemies.Light, Enemies.Light, Enemies.Armed, Enemies.Armed, Enemies.Armed, 
                Enemies.Light, Enemies.Light, Enemies.Light, Enemies.Armed, Enemies.Armed, Enemies.Armed, 
            }, TimeSpan.FromSeconds(30)
        ));
        waves.Add(new Wave( //wave 5
            new[]
            {
                Enemies.Light, Enemies.Light, Enemies.Light,
                Enemies.Light, Enemies.Light, Enemies.Light,
            }, TimeSpan.FromSeconds(30)
        ));
        waves.Add(new Wave( //wave 6
            new[]
            {
                Enemies.Light, Enemies.Light, 
                Enemies.Light, Enemies.Light, Enemies.LightBoss,
            }, TimeSpan.FromSeconds(30)
        ));
        waves.Add(new Wave( //wave 7
            new[]
            {
                Enemies.Armed, Enemies.Armed, Enemies.Armed, Enemies.Armed, 
                Enemies.Armed, Enemies.Armed, Enemies.Armed, Enemies.Armed, 
            }, TimeSpan.FromSeconds(30)    
        ));
        waves.Add(new Wave( //wave 8
            new[]
            {
                Enemies.Light, Enemies.Light, Enemies.ArmedBoss,
                Enemies.Light, Enemies.Light, 
            }, TimeSpan.FromSeconds(30)    
        ));
        Level1 = waves;
        #endregion
        
        #region Level 2

        var level2Waves = new List<Wave>();
        
        level2Waves.Add(new Wave(
            Enemy.Add(
                Enemies.Light * 3
            ).Shuffle(),
            TimeSpan.FromSeconds(25)
            ));
        level2Waves.Add(new Wave(
            Enemy.Add(
                Enemies.Light * 5
            ).Shuffle(),
            TimeSpan.FromSeconds(25)
        ));
        level2Waves.Add(new Wave(
            Enemy.Add(
                Enemies.Light * 6,
                Enemies.Armed * 1
            ).Shuffle(),
            TimeSpan.FromSeconds(25)
        ));
        level2Waves.Add(new Wave(
            Enemy.Add(
                Enemies.Light * 6,
                Enemies.Armed * 2
            ).Shuffle(),
            TimeSpan.FromSeconds(25)
        ));
        level2Waves.Add(new Wave(
            Enemy.Add(
                Enemies.Light * 12,
                Enemies.Armed * 3
            ).Shuffle(),
            TimeSpan.FromSeconds(25)
        ));
        level2Waves.Add(new Wave(
            Enemy.Add(
                Enemies.LightBoss * 1,
                Enemies.Light * 3,
                Enemies.Armed * 1
            ).Shuffle(),
            TimeSpan.FromSeconds(25)
        ));
        level2Waves.Add(new Wave(
            Enemy.Add(
                Enemies.Light * 3,
                Enemies.Armed * 6
            ).Shuffle(),
            TimeSpan.FromSeconds(25)
        ));
        level2Waves.Add(new Wave(
            Enemy.Add(
                Enemies.Light * 3,
                Enemies.Armed * 8
            ).Shuffle(),
            TimeSpan.FromSeconds(25)
        ));
        level2Waves.Add(new Wave(
            Enemy.Add(
                Enemies.Light * 5,
                Enemies.Armed * 12
            ).Shuffle(),
            TimeSpan.FromSeconds(25)
        ));
        level2Waves.Add(new Wave(
            Enemy.Add(
                Enemies.Armed * 3,
                Enemies.ArmedBoss * 1
            ).Shuffle(),
            TimeSpan.FromSeconds(25)
        ));
        level2Waves.Add(new Wave(
            Enemy.Add(
                Enemies.LightBoss * 2,
                Enemies.Light * 3,
                Enemies.Armed * 6
            ).Shuffle(),
            TimeSpan.FromSeconds(25)
        ));
        level2Waves.Add(new Wave(
            Enemy.Add(
                Enemies.LightBoss * 2,
                Enemies.ArmedBoss * 2
            ).Shuffle(),
            TimeSpan.FromSeconds(30)
        ));
        level2Waves.Add(new Wave(
            Enemy.Add(
                Enemies.LightBoss * 6,
                Enemies.ArmedBoss * 6,
                Enemies.Light * 12,
                Enemies.Armed * 4
            ).Shuffle(),
            TimeSpan.FromSeconds(25)
        ));
        
        
        Level2 = level2Waves;
        
        #endregion
        
        #region level 3
        
        var level3Waves = new List<Wave>();
        
        level3Waves.Add(new Wave(
            Enemy.Add(
                Enemies.Light * 4
            ).Shuffle(),
            TimeSpan.FromSeconds(15)
            ));
        level3Waves.Add(new Wave(
            Enemy.Add(
                Enemies.Light * 4,
                Enemies.Armed * 4
            ).Shuffle(),
            TimeSpan.FromSeconds(15)
        ));
        level3Waves.Add(new Wave(
            Enemy.Add(
                Enemies.Light * 8,
                Enemies.Armed * 2
            ).Shuffle(),
            TimeSpan.FromSeconds(15)
        ));
        level3Waves.Add(new Wave(
            Enemy.Add(
                Enemies.Light * 10,
                Enemies.Armed * 2
            ).Shuffle(),
            TimeSpan.FromSeconds(15)
        ));
        level3Waves.Add(new Wave(
            Enemy.Add(
                Enemies.Light * 6,
                Enemies.Armed * 8
            ).Shuffle(),
            TimeSpan.FromSeconds(15)
        ));
        level3Waves.Add(new Wave(
            Enemy.Add(
                Enemies.LightBoss * 1,
                Enemies.Light * 8
            ).Shuffle(),
            TimeSpan.FromSeconds(15)
        ));
        level3Waves.Add(new Wave(
            Enemy.Add(
                Enemies.LightBoss * 1,
                Enemies.Light * 4,
                Enemies.Armed * 4
            ).Shuffle(),
            TimeSpan.FromSeconds(15)
        ));
        level3Waves.Add(new Wave(
            Enemy.Add(
                Enemies.LightBoss * 2,
                
                Enemies.Armed * 6
            ).Shuffle(),
            TimeSpan.FromSeconds(15)
        ));
        level3Waves.Add(new Wave(
            Enemy.Add(
                Enemies.ArmedBoss * 1,
                Enemies.LightBoss * 2,
                Enemies.Armed * 1
            ).Shuffle(),
            TimeSpan.FromSeconds(15)
        ));
        level3Waves.Add(new Wave(
            Enemy.Add(
                Enemies.ArmedBoss * 4,
                Enemies.LightBoss * 4,
                Enemies.Armed * 8
            ).Shuffle(),
            TimeSpan.FromSeconds(15)
        ));

        Level3 = level3Waves;
        
        #endregion
        
        #region Level 4
        
        var level4Waves = new List<Wave>();
        
        level4Waves.Add(new Wave(
            Enemy.Add(
                Enemies.Light * 5
            ).Shuffle(),
            TimeSpan.FromSeconds(12.5)
            ));
        level4Waves.Add(new Wave(
            Enemy.Add(
                Enemies.Light * 5,
                Enemies.Armed * 2
            ).Shuffle(),
            TimeSpan.FromSeconds(12.5)
        ));
        level4Waves.Add(new Wave(
            Enemy.Add(
                Enemies.Light * 7,
                Enemies.Armed * 1
            ).Shuffle(),
            TimeSpan.FromSeconds(8)
        ));
        level4Waves.Add(new Wave(
            Enemy.Add(
                Enemies.Light * 8,
                Enemies.Armed * 1
            ).Shuffle(),
            TimeSpan.FromSeconds(7)
        ));
        level4Waves.Add(new Wave(
            Enemy.Add(
                Enemies.Light * 9,
                Enemies.Armed * 1
            ).Shuffle(),
            TimeSpan.FromSeconds(6)
        ));
        level4Waves.Add(new Wave(
            Enemy.Add(
                Enemies.Light * 10,
                Enemies.Armed * 1
            ).Shuffle(),
            TimeSpan.FromSeconds(30)
        ));
        level4Waves.Add(new Wave(
            Enemy.Add(
                Enemies.Light * 6,
                Enemies.Armed * 7
            ).Shuffle(),
            TimeSpan.FromSeconds(12.5)
        ));
        level4Waves.Add(new Wave(
            Enemy.Add(
                Enemies.LightBoss * 1,
                Enemies.Armed * 4
            ).Shuffle(),
            TimeSpan.FromSeconds(12.5)
        ));
        level4Waves.Add(new Wave(
            Enemy.Add(
                Enemies.LightBoss * 1,
                Enemies.Armed * 6
            ).Shuffle(),
            TimeSpan.FromSeconds(10)
        ));
        level4Waves.Add(new Wave(
            Enemy.Add(
                Enemies.Armed * 8
            ).Shuffle(),
            TimeSpan.FromSeconds(9)
        ));
        level4Waves.Add(new Wave(
            Enemy.Add(
                Enemies.Armed * 9
            ).Shuffle(),
            TimeSpan.FromSeconds(8)
        ));
        level4Waves.Add(new Wave(
            Enemy.Add(
                Enemies.Armed * 8
            ).Shuffle(),
            TimeSpan.FromSeconds(7)
        ));
        level4Waves.Add(new Wave(
            Enemy.Add(
                Enemies.ArmedBoss * 1,
                Enemies.Light * 5,
                Enemies.Armed * 4
            ).Shuffle(),
            TimeSpan.FromSeconds(30)
        ));
        level4Waves.Add(new Wave(
            Enemy.Add(
                Enemies.ArmedBoss * 2,
                Enemies.LightBoss * 1,
                Enemies.Armed * 4
            ).Shuffle(),
            TimeSpan.FromSeconds(12.5)
        ));
        level4Waves.Add(new Wave(
            Enemy.Add(
                Enemies.ArmedBoss * 2,
                Enemies.LightBoss * 3,
                Enemies.Armed * 2
            ).Shuffle(),
            TimeSpan.FromSeconds(10)
        ));
        level4Waves.Add(new Wave(
            Enemy.Add(
                Enemies.ArmedBoss * 3,
                Enemies.LightBoss * 4
            ).Shuffle(),
            TimeSpan.FromSeconds(15)
        ));
        level4Waves.Add(new Wave(
            Enemy.Add(
                Enemies.ArmedBoss * 8
            ).Shuffle(),
            TimeSpan.FromSeconds(10)
        ));//10
        level4Waves.Add(new Wave(
            Enemy.Add(
                Enemies.ArmedBoss * 8,
                Enemies.LightBoss * 8,
                Enemies.Armed * 4,
                Enemies.Light * 8
            ).Shuffle(),
            TimeSpan.FromSeconds(10)
        ));
        level4Waves.Add(new Wave(
            Enemy.Add(
                Enemies.ArmedBoss * 4,
                Enemies.LightBoss * 4,
                Enemies.Armed * 8,
                Enemies.Light * 8
            ).Shuffle(),
            TimeSpan.FromSeconds(10)
        ));
        level4Waves.Add(new Wave(
            Enemy.Add(
                Enemies.ArmedBoss * 2,
                Enemies.LightBoss * 2,
                Enemies.Armed * 16,
                Enemies.Light * 16
            ).Shuffle(),
            TimeSpan.FromSeconds(10)
        ));
        level4Waves.Add(new Wave(
            Enemy.Add(
                Enemies.Armed * 16,
                Enemies.Light * 32
            ).Shuffle(),
            TimeSpan.FromSeconds(10)
        ));

        Level4 = level4Waves;

        #endregion

        current = this;
    }
}
