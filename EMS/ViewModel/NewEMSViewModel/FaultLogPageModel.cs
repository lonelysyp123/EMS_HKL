﻿using CommunityToolkit.Mvvm.Input;
using EMS.Model;
using EMS.Storage.DB.DBManage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EMS.ViewModel.NewEMSViewModel
{
    public class FaultLogPageModel : ViewModelBase
    {
        #region ObservableObject
        private ObservableCollection<FaultLogModel> faultLogData;
        public ObservableCollection<FaultLogModel> FaultLogData
        {
            get { return faultLogData; }
            set
            {
                SetProperty(ref faultLogData, value);
            }
        }

        private FaultLogModel selectedFaultLogData;
        public FaultLogModel SelectedFaultLogData
        {
            get { return selectedFaultLogData; }
            set
            {
                SetProperty(ref selectedFaultLogData, value);
            }
        }

        private int maxPageCount;
        public int MaxPageCount
        {
            get { return maxPageCount; }
            set
            {
                SetProperty(ref maxPageCount, value);
            }
        }

        private int pageIndex;
        public int PageIndex
        {
            get { return pageIndex; }
            set
            {
                SetProperty(ref pageIndex, value);
            }
        }

        private int dataCountPerPage;
        public int DataCountPerPage
        {
            get { return dataCountPerPage; }
            set
            {
                SetProperty(ref dataCountPerPage, value);
            }
        }
        #endregion

        #region Command
        public RelayCommand PageUpdatedCommand { get; private set; }

        #endregion

        private List<FaultLogModel> FaultLogDataByDB;
        public FaultLogPageModel()
        {
            PageUpdatedCommand = new RelayCommand(PageUpdated);

            FaultLogData = new ObservableCollection<FaultLogModel>();
            FaultLogDataByDB = new List<FaultLogModel>();
            DataCountPerPage = 10;
            MaxPageCount = 1;
            PageIndex = 1;
            InitView();
        }

        public void InsertFaultLogData(FaultLogModel model)
        {
            FaultLogDataByDB.Add(model);
            if (FaultLogDataByDB.Count %10 > 1)
            {
                MaxPageCount = FaultLogDataByDB.Count / 10 + 1;
            }
            else
            {
                MaxPageCount = FaultLogDataByDB.Count / 10;
            }
        }

        private void PageUpdated()
        {
            FaultLogData = new ObservableCollection<FaultLogModel>(FaultLogDataByDB.GetRange((PageIndex - 1) * DataCountPerPage, DataCountPerPage));
        }

        public void InitView()
        {
            AlarmandFaultInfoManage manage = new AlarmandFaultInfoManage();
            var entities = manage.Get();
            int i = 0;
            if (entities != null && entities.Count > 0)
            {
                foreach (var entity in entities)
                {
                    i++;
                    FaultLogModel faultLogModel = new FaultLogModel()
                    {
                        FaultNumber = i.ToString(),
                        FaultDevice = entity.Device,
                        FaultId = entity.id.ToString(),
                        FaultModule = entity.Module.ToString(),
                        FaultTime = entity.HappenTime.ToString(),
                        FaultName = entity.Type.ToString(),
                        FaultGrade = entity.level.ToString()
                    };
                    FaultLogDataByDB.Add(faultLogModel);
                }

                if (FaultLogDataByDB.Count % 10 > 1)
                {
                    MaxPageCount = FaultLogDataByDB.Count / 10 + 1;
                }
                else
                {
                    MaxPageCount = FaultLogDataByDB.Count / 10;
                }
                PageUpdated();
            }
        }
    }
}
