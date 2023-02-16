// using System.Collections.Generic;
// using Unity.VisualScripting;
// using UnityEngine;
//
// namespace Towers
// {
//     public interface IAudioPlayable
//     {
//         protected AudioSource player { get; set; }
//     
//         protected void PlayOne(AudioClip clip)
//         {
//             player.PlayOneShot(clip);
//         }
//
//         protected void PlayRandom(List<AudioClip> clips)
//         {
//             var rnd = new System.Random();
//             
//             player.PlayOneShot(clips[rnd.Next(0, clips.Count-1)]);
//         }
//     }
// }