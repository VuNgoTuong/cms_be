using AutoMapper;
using Common;
using Common.Commons;
using Common.Params.Base;
using Repository.CustomModel;
using Repository.Model;
using Repository.Repositories;
using System;
using System.Threading.Tasks;
using UserManagement.Common;
using UserManagement.Config;
using UserManagement.Models.Main;

namespace UserManagement.Services.Implement.GeneralSetting
{
    public class TenantService : BaseService, ITenantService
    {
        private readonly ITenantRepository _tenantRepository;
        public TenantService(
            ITenantRepository tenantRepository,
            ILogger logger,
            IConfigManager config,
            IMapper mapper) : base(config, logger, mapper)
        {
            _tenantRepository = tenantRepository;
        }

        public async Task<ResponseService<ListResult<TenantResponse>>> GetAllCustom(PagingParamCustom param)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                param.group_id = SessionStore.Get<Guid>(Constants.KEY_SESSION_GROUP_ID);

                ListResult<QTTS01_Tenant> tenantModel = await _tenantRepository.GetAllCustom(param);
                ListResult<TenantResponse> result = _mapper.Map<ListResult<QTTS01_Tenant>, ListResult<TenantResponse>>(tenantModel);

                return new ResponseService<ListResult<TenantResponse>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ListResult<TenantResponse>>(ex);
            }
        }

        public async Task<ResponseService<TenantResponse>> GetById(Guid id)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                Guid groupId = SessionStore.Get<Guid>(Constants.KEY_SESSION_GROUP_ID);

                QTTS01_Tenant data = await _tenantRepository.GetSingle(x => x.id == id);
                TenantResponse result = _mapper.Map<QTTS01_Tenant, TenantResponse>(data);

                return new ResponseService<TenantResponse>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<TenantResponse>(ex);
            }
        }

        public async Task<ResponseService<TenantResponse>> Create(TenantRequest obj)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                obj.AddInfo();

                bool checkExistsTenantName = await _tenantRepository.CheckExistsAnyAsync(x => x.tenant_name.ToLower() == obj.tenant_name.Trim().ToLower() && x.group_id == obj.tenant_id);
                if (checkExistsTenantName)
                {
                    return new ResponseService<TenantResponse>(Constants.TENANT_NAME_IS_ALREADY_EXISTS).BadRequest(MessCodes.BANK_NAME_IS_ALREADY_EXISTS);
                }

                QTTS01_Tenant entity = _mapper.Map<TenantRequest, QTTS01_Tenant>(obj);
                await _tenantRepository.Create(entity);

                TenantResponse result = _mapper.Map<QTTS01_Tenant, TenantResponse>(entity);
                return new ResponseService<TenantResponse>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<TenantResponse>(ex);
            }
        }

        public async Task<ResponseService<TenantResponse>> Update(TenantRequest obj)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

                obj.UpdateInfo();

                QTTS01_Tenant checkExists = await _tenantRepository.GetSingle(x => x.id == obj.id && x.group_id == obj.tenant_id);
                if (checkExists == null)
                {
                    return new ResponseService<TenantResponse>(Constants.TENANT_NOT_FOUND).BadRequest(MessCodes.TENANT_NOT_FOUND);
                }

                bool checkExistsName = await _tenantRepository.CheckExistsAnyAsync(x => x.id != obj.id && x.group_id == obj.tenant_id);
                if (checkExists == null)
                {
                    return new ResponseService<TenantResponse>(Constants.BANK_NOT_FOUND).BadRequest(MessCodes.BANK_NOT_FOUND);
                }

                QTTS01_Tenant entity = _mapper.Map<TenantRequest, QTTS01_Tenant>(obj);
                entity.create_by = checkExists.create_by;
                entity.create_time = checkExists.create_time;
                entity.is_active = checkExists.is_active;

                await _tenantRepository.Update(entity, obj.id);
                TenantResponse result = _mapper.Map<QTTS01_Tenant, TenantResponse>(entity);

                return new ResponseService<TenantResponse>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<TenantResponse>(ex);
            }
        }

        public async Task<ResponseService<bool>> Delete(Guid id)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                Guid groupId = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);

                QTTS01_Tenant checkExists = await _tenantRepository.GetSingle(x => x.id == id && x.group_id == groupId);
                if (checkExists == null)
                {
                    return new ResponseService<bool>(Constants.TENANT_NOT_FOUND).BadRequest(MessCodes.TENANT_NOT_FOUND);
                }

                await _tenantRepository.DeleteCustom(checkExists);

                return new ResponseService<bool>(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<bool>(ex);
            }
        }
    }
}
