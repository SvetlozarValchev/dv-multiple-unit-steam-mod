using System;
using System.Collections.Generic;
using System.Reflection;
using UnityModManagerNet;
using Harmony12;
using UnityEngine;

namespace MutlipleUnitSteam
{
    public class Main
    {
        static bool Load(UnityModManager.ModEntry modEntry)
        {
            var harmony = HarmonyInstance.Create(modEntry.Info.Id);
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            return true;
        }
    }

    // sander & fan
    //[HarmonyPatch(typeof(CabInputSteam), "OnEnable")]
    //class CabInputSteam_OnEnable_Patch2
    //{
    //    static CabInputSteam instance;

    //    static void Postfix(CabInputSteam __instance)
    //    {
    //        instance = __instance;

    //        __instance.StartCoroutine(AttachListeners());
    //    }

    //    static IEnumerator<object> AttachListeners()
    //    {
    //        yield return WaitFor.SecondsRealtime(1.5f);

    //        DV.CabControls.ControlImplBase brakeControlCtrl = instance.brake.GetComponent<DV.CabControls.ControlImplBase>();

    //        brakeControlCtrl.ValueChanged += (e =>
    //        {
    //            if (PlayerManager.Trainset == null) return;

    //            for (int i = 0; i < PlayerManager.Trainset.cars.Count; i++)
    //            {
    //                TrainCar car = PlayerManager.Trainset.cars[i];

    //                if (PlayerManager.Car.Equals(car))
    //                {
    //                    continue;
    //                }

    //                if (car.carType == TrainCarType.LocoSteamHeavy)
    //                {
    //                    LocoControllerSteam steamController = car.GetComponent<LocoControllerSteam>();

    //                    if (steamController)
    //                    {
    //                        steamController.SetBrake(e.newValue);
    //                    }
    //                }
    //            }
    //        });

    //        DV.CabControls.ControlImplBase throttleControl = instance.throttle.GetComponent<DV.CabControls.ControlImplBase>();

    //        throttleControl.ValueChanged += (e =>
    //        {
    //            if (PlayerManager.Trainset == null) return;

    //            for (int i = 0; i < PlayerManager.Trainset.cars.Count; i++)
    //            {
    //                TrainCar car = PlayerManager.Trainset.cars[i];

    //                if (PlayerManager.Car.Equals(car))
    //                {
    //                    continue;
    //                }

    //                if (car.carType == TrainCarType.LocoSteamHeavy)
    //                {
    //                    LocoControllerSteam steamController = car.GetComponent<LocoControllerSteam>();

    //                    if (steamController)
    //                    {
    //                        steamController.SetThrottle(e.newValue);
    //                    }
    //                }
    //            }
    //        });
    //    }
    //}

    [HarmonyPatch(typeof(LocoControllerSteam), "SetThrottle")]
    class LocoControllerSteam_SetThrottle_Patch
    {
        static void Postfix(LocoControllerSteam __instance, float throttle)
        {
            TrainCar currentCar = __instance.GetComponent<TrainCar>();
            TrainCar targetCar = null;
            Trainset trainset = null;

            if (PlayerManager.Car != null && PlayerManager.Car.trainset != null)
            {
                targetCar = PlayerManager.Car;
                trainset = PlayerManager.Car.trainset;
            }

            if (currentCar == null || targetCar == null || !targetCar.Equals(currentCar) || trainset == null || trainset.cars.Count < 2)
            {
                return;
            }

            List<TrainCar> trainsetCars = trainset.cars;

            for (int i = 0; i < trainsetCars.Count; i++)
            {
                TrainCar car = trainsetCars[i];

                if (targetCar.Equals(car))
                {
                    continue;
                }

                if (car.carType == TrainCarType.LocoSteamHeavy)
                {
                    LocoControllerSteam steamController = car.GetComponent<LocoControllerSteam>();

                    if (steamController)
                    {
                        steamController.SetThrottle(throttle);
                    }
                }
            }
        }
    }

    [HarmonyPatch(typeof(LocoControllerBase), "SetBrake")]
    class LocoControllerBase_SetBrake_Patch
    {
        static void Postfix(LocoControllerBase __instance, float nextTargetBrake)
        {
            TrainCar currentCar = __instance.GetComponent<TrainCar>();
            TrainCar targetCar = null;
            Trainset trainset = null;

            if (PlayerManager.Car != null && PlayerManager.Car.trainset != null)
            {
                targetCar = PlayerManager.Car;
                trainset = PlayerManager.Car.trainset;
            }

            if (currentCar == null || targetCar == null || !targetCar.Equals(currentCar) || trainset == null || trainset.cars.Count < 2)
            {
                return;
            }

            List<TrainCar> trainsetCars = trainset.cars;

            for (int i = 0; i < trainsetCars.Count; i++)
            {
                TrainCar car = trainsetCars[i];

                if (targetCar.Equals(car))
                {
                    continue;
                }

                if (car.carType == TrainCarType.LocoSteamHeavy)
                {
                    LocoControllerSteam steamController = car.GetComponent<LocoControllerSteam>();

                    if (steamController)
                    {
                        steamController.SetBrake(nextTargetBrake);
                    }
                }
            }
        }
    }

    [HarmonyPatch(typeof(LocoControllerBase), "SetIndependentBrake")]
    class LocoControllerBase_SetIndependentBrake_Patch
    {
        static void Postfix(LocoControllerBase __instance, float nextTargetIndependentBrake)
        {
            TrainCar currentCar = __instance.GetComponent<TrainCar>();
            TrainCar targetCar = null;
            Trainset trainset = null;

            if (PlayerManager.Car != null && PlayerManager.Car.trainset != null)
            {
                targetCar = PlayerManager.Car;
                trainset = PlayerManager.Car.trainset;
            }

            if (currentCar == null || targetCar == null || !targetCar.Equals(currentCar) || trainset == null || trainset.cars.Count < 2)
            {
                return;
            }

            List<TrainCar> trainsetCars = trainset.cars;

            for (int i = 0; i < trainsetCars.Count; i++)
            {
                TrainCar car = trainsetCars[i];

                if (targetCar.Equals(car))
                {
                    continue;
                }

                if (car.carType == TrainCarType.LocoSteamHeavy)
                {
                    LocoControllerSteam steamController = car.GetComponent<LocoControllerSteam>();

                    if (steamController)
                    {
                        steamController.SetIndependentBrake(nextTargetIndependentBrake);
                    }
                }
            }
        }
    }

    [HarmonyPatch(typeof(LocoControllerSteam), "SetReverser")]
    class LocoControllerSteam_SetReverser_Patch
    {
        static void Postfix(LocoControllerSteam __instance, float position)
        {
            TrainCar currentCar = __instance.GetComponent<TrainCar>();
            TrainCar targetCar = null;
            Trainset trainset = null;

            if (PlayerManager.Car != null && PlayerManager.Car.trainset != null)
            {
                targetCar = PlayerManager.Car;
                trainset = PlayerManager.Car.trainset;
            }

            if (currentCar == null || targetCar == null || !targetCar.Equals(currentCar) || trainset == null || trainset.cars.Count < 2)
            {
                return;
            }

            List<TrainCar> trainsetCars = trainset.cars;

            for (int i = 0; i < trainsetCars.Count; i++)
            {
                TrainCar car = trainsetCars[i];

                if (targetCar.Equals(car))
                {
                    continue;
                }

                if (car.carType == TrainCarType.LocoSteamHeavy)
                {
                    LocoControllerSteam steamController = car.GetComponent<LocoControllerSteam>();

                    if (steamController)
                    {
                        if (GetCarsBehind(PlayerManager.Car).Contains(car))
                        {
                            if (GetCarsInFrontOf(car).Contains(PlayerManager.Car))
                            {
                                steamController.SetReverser(position);
                            }
                            else
                            {
                                steamController.SetReverser(position * -1f);
                            }
                        }
                        else if (GetCarsInFrontOf(PlayerManager.Car).Contains(car))
                        {
                            if (GetCarsBehind(car).Contains(PlayerManager.Car))
                            {
                                steamController.SetReverser(position);
                            }
                            else
                            {
                                steamController.SetReverser(position * -1f);
                            }
                        }
                    }
                }
            }
        }

        public static List<TrainCar> GetCarsInFrontOf(TrainCar car)
        {
            return GetCarsCoupledTo(car.frontCoupler);
        }

        public static List<TrainCar> GetCarsBehind(TrainCar car)
        {
            return GetCarsCoupledTo(car.rearCoupler);
        }

        public static List<TrainCar> GetCarsCoupledTo(Coupler coupler)
        {
            List<TrainCar> trainCarList = new List<TrainCar>();
            for (coupler = coupler.GetCoupled(); coupler != null; coupler = coupler.GetOppositeCoupler().GetCoupled())
                trainCarList.Add(coupler.train);
            return trainCarList;
        }
    }

    [HarmonyPatch(typeof(LocoControllerSteam), "SetFireOn")]
    class LocoControllerSteam_SetFireOn_Patch
    {
        static void Postfix(LocoControllerSteam __instance, float percentage)
        {
            TrainCar currentCar = __instance.GetComponent<TrainCar>();
            TrainCar targetCar = null;
            Trainset trainset = null;

            if (PlayerManager.Car != null && PlayerManager.Car.trainset != null)
            {
                targetCar = PlayerManager.Car;
                trainset = PlayerManager.Car.trainset;
            }

            if (currentCar == null || targetCar == null || !targetCar.Equals(currentCar) || trainset == null || trainset.cars.Count < 2)
            {
                return;
            }

            List<TrainCar> trainsetCars = trainset.cars;

            for (int i = 0; i < trainsetCars.Count; i++)
            {
                TrainCar car = trainsetCars[i];

                if (targetCar.Equals(car))
                {
                    continue;
                }

                if (car.carType == TrainCarType.LocoSteamHeavy)
                {
                    LocoControllerSteam steamController = car.GetComponent<LocoControllerSteam>();

                    if (steamController)
                    {
                        steamController.SetFireOn(percentage);
                    }
                }
            }
        }
    }

    [HarmonyPatch(typeof(LocoControllerSteam), "AddCoalChunk")]
    class LocoControllerSteam_AddCoalChunk_Patch
    {
        static void Postfix(LocoControllerSteam __instance)
        {
            TrainCar currentCar = __instance.GetComponent<TrainCar>();
            TrainCar targetCar = null;
            Trainset trainset = null;

            if (PlayerManager.Car != null && PlayerManager.Car.trainset != null)
            {
                targetCar = PlayerManager.Car;
                trainset = PlayerManager.Car.trainset;
            }

            if (currentCar == null || targetCar == null || !targetCar.Equals(currentCar) || trainset == null || trainset.cars.Count < 2)
            {
                return;
            }

            List<TrainCar> trainsetCars = trainset.cars;

            for (int i = 0; i < trainsetCars.Count; i++)
            {
                TrainCar car = trainsetCars[i];

                if (targetCar.Equals(car))
                {
                    continue;
                }

                if (car.carType == TrainCarType.LocoSteamHeavy)
                {
                    LocoControllerSteam steamController = car.GetComponent<LocoControllerSteam>();

                    if (steamController)
                    {
                        steamController.AddCoalChunk();
                    }
                }
            }
        }
    }

    [HarmonyPatch(typeof(LocoControllerSteam), "SetFireDoorOpen")]
    class LocoControllerSteam_SetFireDoorOpen_Patch
    {
        static void Postfix(LocoControllerSteam __instance, float percentage)
        {
            TrainCar currentCar = __instance.GetComponent<TrainCar>();
            TrainCar targetCar = null;
            Trainset trainset = null;

            if (PlayerManager.Car != null && PlayerManager.Car.trainset != null)
            {
                targetCar = PlayerManager.Car;
                trainset = PlayerManager.Car.trainset;
            }

            if (currentCar == null || targetCar == null || !targetCar.Equals(currentCar) || trainset == null || trainset.cars.Count < 2)
            {
                return;
            }

            List<TrainCar> trainsetCars = trainset.cars;

            for (int i = 0; i < trainsetCars.Count; i++)
            {
                TrainCar car = trainsetCars[i];

                if (targetCar.Equals(car))
                {
                    continue;
                }

                if (car.carType == TrainCarType.LocoSteamHeavy)
                {
                    LocoControllerSteam steamController = car.GetComponent<LocoControllerSteam>();

                    if (steamController)
                    {
                        steamController.SetFireDoorOpen(percentage);
                    }
                }
            }
        }
    }

    [HarmonyPatch(typeof(CabInputSteamExtra), "OnEnable")]
    class CabInputSteamExtra_OnEnable_Patch
    {
        static CabInputSteamExtra instance;

        static void Postfix(CabInputSteamExtra __instance)
        {
            instance = __instance;

            __instance.StartCoroutine(AttachListeners());
        }

        static IEnumerator<object> AttachListeners()
        {
            yield return null;

            DV.CabControls.ControlImplBase sanderCtrl = instance.sanderValveObj.GetComponent<DV.CabControls.ControlImplBase>();

            sanderCtrl.ValueChanged += (e =>
            {
                if (PlayerManager.Car == null || PlayerManager.Car.trainset == null) return;

                for (int i = 0; i < PlayerManager.Car.trainset.cars.Count; i++)
                {
                    TrainCar car = PlayerManager.Car.trainset.cars[i];

                    if (PlayerManager.Car.Equals(car))
                    {
                        continue;
                    }

                    if (car.carType == TrainCarType.LocoSteamHeavy)
                    {
                        LocoControllerSteam steamController = car.GetComponent<LocoControllerSteam>();

                        if (steamController)
                        {
                            steamController.SetSanderValve(e.newValue);
                        }
                    }
                }
            });

            DV.CabControls.ControlImplBase blowerCtrl = instance.blowerValveObj.GetComponent<DV.CabControls.ControlImplBase>();

            blowerCtrl.ValueChanged += (e =>
            {
                if (PlayerManager.Car == null || PlayerManager.Car.trainset == null) return;

                for (int i = 0; i < PlayerManager.Car.trainset.cars.Count; i++)
                {
                    TrainCar car = PlayerManager.Car.trainset.cars[i];

                    if (PlayerManager.Car.Equals(car))
                    {
                        continue;
                    }

                    if (car.carType == TrainCarType.LocoSteamHeavy)
                    {
                        LocoControllerSteam steamController = car.GetComponent<LocoControllerSteam>();

                        if (steamController)
                        {
                            steamController.SetBlower(e.newValue);
                        }
                    }
                }
            });

            DV.CabControls.ControlImplBase injectorCtrl = instance.injectorValveObj.GetComponent<DV.CabControls.ControlImplBase>();

            injectorCtrl.ValueChanged += (e =>
            {
                if (PlayerManager.Car == null || PlayerManager.Car.trainset == null) return;

                for (int i = 0; i < PlayerManager.Car.trainset.cars.Count; i++)
                {
                    TrainCar car = PlayerManager.Car.trainset.cars[i];

                    if (PlayerManager.Car.Equals(car))
                    {
                        continue;
                    }

                    if (car.carType == TrainCarType.LocoSteamHeavy)
                    {
                        LocoControllerSteam steamController = car.GetComponent<LocoControllerSteam>();

                        if (steamController)
                        {
                            steamController.SetInjector(e.newValue);
                        }
                    }
                }
            });

            DV.CabControls.ControlImplBase waterDumpCtrl = instance.waterDumpValveObj.GetComponent<DV.CabControls.ControlImplBase>();

            waterDumpCtrl.ValueChanged += (e =>
            {
                if (PlayerManager.Car == null || PlayerManager.Car.trainset == null) return;

                for (int i = 0; i < PlayerManager.Car.trainset.cars.Count; i++)
                {
                    TrainCar car = PlayerManager.Car.trainset.cars[i];

                    if (PlayerManager.Car.Equals(car))
                    {
                        continue;
                    }

                    if (car.carType == TrainCarType.LocoSteamHeavy)
                    {
                        LocoControllerSteam steamController = car.GetComponent<LocoControllerSteam>();

                        if (steamController)
                        {
                            steamController.SetWaterDump(e.newValue);
                        }
                    }
                }
            });

            DV.CabControls.ControlImplBase steamReleaseCtrl = instance.steamReleaserValveObj.GetComponent<DV.CabControls.ControlImplBase>();

            steamReleaseCtrl.ValueChanged += (e =>
            {
                if (PlayerManager.Car == null || PlayerManager.Car.trainset == null) return;

                for (int i = 0; i < PlayerManager.Car.trainset.cars.Count; i++)
                {
                    TrainCar car = PlayerManager.Car.trainset.cars[i];

                    if (PlayerManager.Car.Equals(car))
                    {
                        continue;
                    }

                    if (car.carType == TrainCarType.LocoSteamHeavy)
                    {
                        LocoControllerSteam steamController = car.GetComponent<LocoControllerSteam>();

                        if (steamController)
                        {
                            steamController.SetSteamReleaser(e.newValue);
                        }
                    }
                }
            });

            DV.CabControls.ControlImplBase draftPullerCtrl = instance.draftPullerObj.GetComponent<DV.CabControls.ControlImplBase>();

            draftPullerCtrl.ValueChanged += (e =>
            {
                if (PlayerManager.Car == null || PlayerManager.Car.trainset == null) return;

                for (int i = 0; i < PlayerManager.Car.trainset.cars.Count; i++)
                {
                    TrainCar car = PlayerManager.Car.trainset.cars[i];

                    if (PlayerManager.Car.Equals(car))
                    {
                        continue;
                    }

                    if (car.carType == TrainCarType.LocoSteamHeavy)
                    {
                        LocoControllerSteam steamController = car.GetComponent<LocoControllerSteam>();

                        if (steamController)
                        {
                            steamController.SetDraft(e.newValue);
                        }
                    }
                }
            });

            DV.CabControls.ControlImplBase fireDoorLeverCtrl = instance.fireDoorLeverObj.GetComponent<DV.CabControls.ControlImplBase>();

            fireDoorLeverCtrl.ValueChanged += (e =>
            {
                if (PlayerManager.Car == null || PlayerManager.Car.trainset == null) return;

                for (int i = 0; i < PlayerManager.Car.trainset.cars.Count; i++)
                {
                    TrainCar car = PlayerManager.Car.trainset.cars[i];

                    if (PlayerManager.Car.Equals(car))
                    {
                        continue;
                    }

                    if (car.carType == TrainCarType.LocoSteamHeavy)
                    {
                        LocoControllerSteam steamController = car.GetComponent<LocoControllerSteam>();

                        if (steamController)
                        {
                            steamController.SetFireDoorOpen(e.newValue);
                        }
                    }
                }
            });
        }
    }
}
 