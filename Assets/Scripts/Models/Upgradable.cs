// using System;
// using Microsoft.CSharp;
//
// namespace Models
// {
//     public enum UpgradeMethod
//     {
//         Add,
//         Substract
//     }
//     public enum UpgradingMethod
//     {
//         Scaling,
//         Addition
//     }
//
//     public class Upgradable<T>
//     {
//         
//
//
//         public T Value { get; set; }
//         public UpgradeMethod UpgradeMethod;
//
//         public Upgradable(T initialValue, UpgradeMethod upgradeMethod = UpgradeMethod.Add)
//         {
//             Value = initialValue;
//             UpgradeMethod = upgradeMethod;
//         }
//
//         public string GetFormatString(dynamic value, UpgradingMethod upgradingMethod, bool isPercent = false)
//         {
//             dynamic newValue;
//             if (upgradingMethod == UpgradingMethod.Scaling)
//             {
//                 newValue = GetPercentValue((float)value);
//             }
//             else if (upgradingMethod == UpgradingMethod.Addition)
//             {
//                 newValue = GetAddedValue(value);
//             }
//             else newValue = default;
//
//             char percent = isPercent ? '%' : '\0';
//             char addChar = UpgradeMethod == UpgradeMethod.Add ? '+' : '-';
//
//             return $"{Value}{percent} {addChar} {newValue - Value}";
//         }
//
//         public T GetPercentValue(float percent)
//         {
//             switch (UpgradeMethod)
//             {
//                 case UpgradeMethod.Add:
//                     return (T)( Value + ((dynamic)Value * percent));
//                 case UpgradeMethod.Substract:
//                     return (T)( Value - ((dynamic)Value * percent));
//             }
//
//             return default(T);
//         }
//         public T GetAddedValue(dynamic value)
//         {
//             switch (UpgradeMethod)
//             {
//                 case UpgradeMethod.Add:
//                     return Value + value;
//                 case UpgradeMethod.Substract:
//                     return Value - value;
//             }
//
//             return default(T);
//         }
//
//
//         public void UpgradePercent(float scale)
//         {
//             switch (UpgradeMethod)
//             {
//                 case UpgradeMethod.Add:
//                     Value +=((dynamic)Value * scale);
//                     break;
//                 case UpgradeMethod.Substract:
//                     Value -=((dynamic)Value * scale);
//                     break;
//             }
//         }
//
//         public void UpgradeAdd(dynamic value)
//         {
//             switch (UpgradeMethod)
//             {
//                 case UpgradeMethod.Add:
//                     Value += value;
//                     break;
//                 case UpgradeMethod.Substract:
//                     Value -= value;
//                     break;
//             }
//         }
//     }
// }