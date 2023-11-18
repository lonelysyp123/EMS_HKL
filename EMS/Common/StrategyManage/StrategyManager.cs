﻿using EMS.Common.Modbus.ModbusTCP;
using EMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EMS.ViewModel
{
    public class StrategyManager
    {
        private static object syncRoot = new Object();

        private static StrategyManager instance;
        public static StrategyManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock(syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new StrategyManager();
                        }
                    }
                }
                return instance;
            }
        }

        private StrategyManager()
        {

        }

        /// <summary>
        /// 峰谷策略
        /// 1. 储能系统充电
        /// 当回收电池开始放电时，将放电电流充到储能系统中
        /// 2. 储能系统放电
        /// 判断时段，在峰时段且需要向回收电池充电的时候开始放电
        /// 
        /// 使用场景
        /// 在PCS实时监控线程中运行
        /// </summary>
        /// <param name="array1">电网端峰谷电价时段</param>
        /// <param name="array2">回收电池端充放电时段</param>
        /// <param name="client">通讯客户端</param>
        public void PVStrategy(List<BatteryStrategyModel> array2, ModbusClient client)
        {
            bool b_obj = true;
            // 判断电池端充放电时段
            DateTime now = DateTime.Now;
            var items = array2.Where(s=> DateTime.Parse(s.StartTime) < now && DateTime.Parse(s.EndTime) >= now);
            if (items.Count() == 1)
            {
                BatteryStrategyModel currentStrategy = items.ToArray()[0];
                if (currentStrategy.BatteryStrategy == BatteryStrategyEnum.ConstantCurrentCharge)
                {
                    if (ReadCurrentSOC() > 20)
                    {
                        if (ReadPCSMode() != 0 || ReadPCSCurrent() != currentStrategy.SetValue)
                        {
                            SetPCS();
                        }
                    }
                    else
                    {
                        if (ReadPCSMode() != 0 || ReadPCSCurrent() != 0)
                        {
                            SetPCS();
                        }
                    }
                }
                else if (currentStrategy.BatteryStrategy == BatteryStrategyEnum.ConstantCurrentDischarge)
                {
                    if (ReadCurrentSOC() < 80)
                    {
                        if (ReadPCSMode() != 0 || ReadPCSCurrent() != currentStrategy.SetValue)
                        {
                            SetPCS();
                        }
                    } 
                    else
                    {
                        if (ReadPCSMode() != 0 || ReadPCSCurrent() != 0)
                        {
                            SetPCS();
                        }
                    }
                }
                else if (currentStrategy.BatteryStrategy == BatteryStrategyEnum.ConstantPowerCharge)
                {
                    if (ReadCurrentSOC() > 20)
                    {
                        if (ReadPCSMode() != 1 || ReadPCSPower() != currentStrategy.SetValue)
                        {
                            SetPCS();
                        }
                    }
                    else
                    {
                        if (ReadPCSMode() != 1 || ReadPCSPower() != 0)
                        {
                            SetPCS();
                        }
                    }
                }
                else if (currentStrategy.BatteryStrategy == BatteryStrategyEnum.ConstantPowerDischarge)
                {
                    if (ReadCurrentSOC() < 80)
                    {
                        if (ReadPCSMode() != 1 || ReadPCSPower() != currentStrategy.SetValue)
                        {
                            SetPCS();
                        }
                    }
                    else
                    {
                        // 储能系统停止充电
                        if (ReadPCSMode() != 1 || ReadPCSPower() != 0)
                        {
                            SetPCS();
                        }
                    }
                }
            }
        }

        private int ReadPCSPower()
        {
            throw new NotImplementedException();
        }

        private void SetPCS()
        {
            throw new NotImplementedException();
        }

        private int ReadPCSCurrent()
        {
            throw new NotImplementedException();
        }

        private int ReadPCSMode()
        {
            throw new NotImplementedException();
        }

        private double ReadCurrentSOC()
        {
            // 根据所有SOC
            return 0;
        }

        /// <summary>
        /// 逆功率保护策略
        /// 
        /// 使用场景
        /// 在PCS实时监控线程中运行
        /// </summary>
        /// <param name="udc">直流母线电压</param>
        /// <param name="reversePower">逆电设定值</param>
        /// <param name="reverseRate">逆功率设定值</param>
        /// <param name="client">通讯客户端</param>
        public void VoltageBalanceStrategy(double udc, double reversePower, double reverseRate, ModbusClient client)
        {
            if (udc >= reversePower - 10 && udc < reversePower)
            {
                // 将储能下功率Pc设为0
                SetPCS();
            }
            else if (udc >= reversePower)
            {
                // 判断DCDC为充电或者未操作
                if (true)
                {
                    // 储能下功率Pc设为reverseRate*400kW进行充电
                }
                else
                {
                    // 将储能下功率Pc设为0
                }
            }
        }

        /// <summary>
        /// 保护策略
        /// 1. 通讯故障
        /// PCS停机
        /// 2. 设备故障
        ///     a. PCS故障
        ///     PCS停机
        ///     b. BMS故障
        ///     根据故障等级处理
        /// 
        /// 使用场景
        /// 出现故障时运行
        /// </summary>
        /// <param name="faultType">故障类型</param>
        /// <param name="faultLevel">故障等级</param>
        /// <param name="faultSource">故障源</param>
        public void ProtectStrategy(object faultType, int faultLevel, object faultSource)
        {
            // 故障整理，取等级最高的故障，默认

            // 判断故障类型，分为通讯故障和设备故障
            if (true)
            {
                // 通讯故障
                // 只要出现故障就停机
            }
            else
            {
                // 判断故障源
                if (true)
                {
                    // 源自PCS，只要是故障就停机
                }
                else
                {
                    // 源自BMS，根据故障等级来规划处理方案
                    switch (faultLevel)
                    {
                        case 1:
                            // 一级故障需要怎么处理
                            break;
                        case 2:
                            // 二级故障需要怎么处理
                            break;
                        case 3:
                            // 三级故障需要怎么处理
                            break;
                    }
                }
            }
        }
    }
}