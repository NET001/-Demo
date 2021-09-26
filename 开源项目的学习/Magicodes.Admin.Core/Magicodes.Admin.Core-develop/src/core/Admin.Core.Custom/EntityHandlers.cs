using System;
using System.Collections.Generic;
using System.Text;
using Abp;
using Abp.Auditing;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Uow;
using Abp.EntityHistory;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;
using Abp.Extensions;
using Abp.Runtime.Session;
using Abp.Timing;

namespace Magicodes.Admin.Core.Custom
{
    /// <summary>
    /// 修复CURD基类没有自动写入租户Id等数据的问题
    /// </summary>
    public class EntityHandlers :
        IEventHandler<EntityCreatingEventData<EntityBase<int>>>,
        IEventHandler<EntityCreatingEventData<EntityBase<long>>>,
        IEventHandler<EntityCreatingEventData<EntityBase<Guid>>>,
        IEventHandler<EntityDeletingEventData<EntityBase<int>>>,
        IEventHandler<EntityDeletingEventData<EntityBase<long>>>,
        IEventHandler<EntityDeletingEventData<EntityBase<Guid>>>,
        IEventHandler<EntityUpdatingEventData<EntityBase<int>>>,
        IEventHandler<EntityUpdatingEventData<EntityBase<long>>>,
        IEventHandler<EntityUpdatingEventData<EntityBase<Guid>>>,
        ITransientDependency
    {
        /// <summary>
        /// 
        /// </summary>
        public EntityHandlers()
        {
            AbpSession = NullAbpSession.Instance;
        }
        /// <summary>
        /// Reference to <see cref="IUnitOfWorkManager"/>.
        /// </summary>
        public IUnitOfWorkManager UnitOfWorkManager
        {
            get
            {
                if (_unitOfWorkManager == null)
                {
                    throw new AbpException("Must set UnitOfWorkManager before use it.");
                }

                return _unitOfWorkManager;
            }
            set => _unitOfWorkManager = value;
        }
        private IUnitOfWorkManager _unitOfWorkManager;

        public IAbpSession AbpSession { get; set; }

        /// <summary>
        /// Gets current unit of work.
        /// </summary>
        protected IActiveUnitOfWork CurrentUnitOfWork => UnitOfWorkManager.Current;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual int? GetCurrentTenantIdOrNull()
        {
            if (CurrentUnitOfWork != null)
            {
                return CurrentUnitOfWork.GetTenantId();
            }

            return AbpSession.TenantId;
        }

        /// <summary>
        /// 自动设置创建审计字段
        /// </summary>
        /// <param name="entityAsObj"></param>
        public void SetCreationAuditProperties(object entityAsObj)
        {
            switch (entityAsObj)
            {
                case IMayHaveTenant _:
                    {
                        var entity = entityAsObj.As<IMayHaveTenant>();
                        if (entity.TenantId == null || entity.TenantId == 0)
                        {
                            var currentTenantId = GetCurrentTenantIdOrNull();

                            if (currentTenantId != null)
                            {
                                entity.TenantId = currentTenantId.Value;
                            }
                        }

                        break;
                    }
                case IMustHaveTenant _:
                    {
                        var entity = entityAsObj.As<IMustHaveTenant>();
                        if (entity.TenantId == 0)
                        {
                            var currentTenantId = GetCurrentTenantIdOrNull();

                            if (currentTenantId != null)
                            {
                                entity.TenantId = currentTenantId.Value;
                            }
                        }

                        break;
                    }
            }

            if (entityAsObj is IHasCreationTime entityWithCreationTime)
            {
                if (entityWithCreationTime.CreationTime == default(DateTime))
                {
                    entityWithCreationTime.CreationTime = Clock.Now;
                }
            }

            if (entityAsObj is ICreationAudited)
            {
                var entity = entityAsObj as ICreationAudited;
                if (entity.CreatorUserId == null)
                {
                    entity.CreatorUserId = AbpSession.UserId;
                }
            }
        }

        /// <summary>
        /// 自动设置更新审计字段
        /// </summary>
        /// <param name="entityAsObj"></param>
        public void SetModificationAuditProperties(object entityAsObj)
        {
            if (entityAsObj is IHasModificationTime entityWithModificationTime)
            {
                if (entityWithModificationTime.LastModificationTime == default(DateTime))
                {
                    entityWithModificationTime.LastModificationTime = Clock.Now;
                }
            }

            if (entityAsObj is IModificationAudited)
            {
                var entity = entityAsObj as IModificationAudited;
                if (entity.LastModifierUserId == null)
                {
                    entity.LastModifierUserId = AbpSession.UserId;
                }
            }
        }

        /// <summary>
        /// 自动设置删除审计字段
        /// </summary>
        /// <param name="entityAsObj"></param>
        public void SetDeletionAuditProperties(object entityAsObj)
        {
            if (entityAsObj is IHasDeletionTime entityWithDeletionTime)
            {
                if (entityWithDeletionTime.DeletionTime == default(DateTime))
                {
                    entityWithDeletionTime.DeletionTime = Clock.Now;
                }
            }

            if (entityAsObj is IDeletionAudited)
            {
                var entity = entityAsObj as IDeletionAudited;
                if (entity.DeleterUserId == null)
                {
                    entity.DeleterUserId = AbpSession.UserId;
                }
            }
        }

        public void HandleEvent(EntityCreatingEventData<EntityBase<int>> eventData)
        {
            SetCreationAuditProperties(eventData.Entity);
        }


        /// <summary>
        /// Handler handles the event by implementing this method.
        /// </summary>
        /// <param name="eventData">Event data</param>
        public void HandleEvent(EntityCreatingEventData<EntityBase<long>> eventData)
        {
            SetCreationAuditProperties(eventData.Entity);
        }

        /// <summary>
        /// Handler handles the event by implementing this method.
        /// </summary>
        /// <param name="eventData">Event data</param>
        public void HandleEvent(EntityCreatingEventData<EntityBase<Guid>> eventData)
        {
            SetCreationAuditProperties(eventData.Entity);
        }

        /// <summary>
        /// Handler handles the event by implementing this method.
        /// </summary>
        /// <param name="eventData">Event data</param>
        public void HandleEvent(EntityDeletingEventData<EntityBase<int>> eventData)
        {
            SetDeletionAuditProperties(eventData.Entity);
        }

        /// <summary>
        /// Handler handles the event by implementing this method.
        /// </summary>
        /// <param name="eventData">Event data</param>
        public void HandleEvent(EntityDeletingEventData<EntityBase<long>> eventData)
        {
            SetDeletionAuditProperties(eventData.Entity);
        }

        /// <summary>
        /// Handler handles the event by implementing this method.
        /// </summary>
        /// <param name="eventData">Event data</param>
        public void HandleEvent(EntityDeletingEventData<EntityBase<Guid>> eventData)
        {
            SetDeletionAuditProperties(eventData.Entity);
        }

        /// <summary>
        /// Handler handles the event by implementing this method.
        /// </summary>
        /// <param name="eventData">Event data</param>
        public void HandleEvent(EntityUpdatingEventData<EntityBase<int>> eventData)
        {
            SetModificationAuditProperties(eventData.Entity);
        }

        /// <summary>
        /// Handler handles the event by implementing this method.
        /// </summary>
        /// <param name="eventData">Event data</param>
        public void HandleEvent(EntityUpdatingEventData<EntityBase<long>> eventData)
        {
            SetModificationAuditProperties(eventData.Entity);
        }

        /// <summary>
        /// Handler handles the event by implementing this method.
        /// </summary>
        /// <param name="eventData">Event data</param>
        public void HandleEvent(EntityUpdatingEventData<EntityBase<Guid>> eventData)
        {
            SetModificationAuditProperties(eventData.Entity);

        }
    }
}
