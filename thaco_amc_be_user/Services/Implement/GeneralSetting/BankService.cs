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
    public class BankService : BaseService, IBankService
    {
        private readonly IBankRepository _bankRepository;
        public BankService(
            IBankRepository bankRepository,
            ILogger logger,
            IConfigManager config,
            IMapper mapper) : base(config, logger, mapper)
        {
            _bankRepository = bankRepository;
        }

        public async Task<ResponseService<ListResult<BankResponse>>> GetAll(PagingParam param)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                param.tenant_id = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);

                ListResult<QTTS01_Bank> bankModel = await _bankRepository.GetAll(param);
                ListResult<BankResponse> result = _mapper.Map<ListResult<QTTS01_Bank>, ListResult<BankResponse>>(bankModel);

                return new ResponseService<ListResult<BankResponse>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ListResult<BankResponse>>(ex);
            }
        }

        public async Task<ResponseService<BankResponse>> GetById(Guid id)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                Guid tenantId = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);

                QTTS01_Bank data = await _bankRepository.GetSingle(x => x.id == id && x.tenant_id == tenantId);
                BankResponse result = _mapper.Map<QTTS01_Bank, BankResponse>(data);

                return new ResponseService<BankResponse>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<BankResponse>(ex);
            }
        }

        public async Task<ResponseService<BankResponse>> Create(BankRequest obj)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                obj.AddInfo();

                bool checkExistsBankName = await _bankRepository.CheckExistsAnyAsync(x => x.bank_name.ToLower() == obj.bank_name.Trim().ToLower() && x.tenant_id == obj.tenant_id);
                if (checkExistsBankName)
                {
                    return new ResponseService<BankResponse>(Constants.BANK_NAME_IS_ALREADY_EXISTS).BadRequest(MessCodes.BANK_NAME_IS_ALREADY_EXISTS);
                }

                QTTS01_Bank entity = _mapper.Map<BankRequest, QTTS01_Bank>(obj);
                await _bankRepository.Create(entity);

                BankResponse result = _mapper.Map<QTTS01_Bank, BankResponse>(entity);
                return new ResponseService<BankResponse>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<BankResponse>(ex);
            }
        }

        public async Task<ResponseService<BankResponse>> Update(BankRequest obj)
        {
            try
            {
                obj.UpdateInfo();
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

                QTTS01_Bank checkExists = await _bankRepository.GetSingle(x => x.id == obj.id && x.tenant_id == obj.tenant_id);
                if (checkExists == null)
                {
                    return new ResponseService<BankResponse>(Constants.BANK_NOT_FOUND).BadRequest(MessCodes.BANK_NOT_FOUND);
                }

                bool checkExistsName = await _bankRepository.CheckExistsAnyAsync(x => x.id != obj.id && x.tenant_id == obj.tenant_id);
                if (checkExists == null)
                {
                    return new ResponseService<BankResponse>(Constants.BANK_NOT_FOUND).BadRequest(MessCodes.BANK_NOT_FOUND);
                }

                QTTS01_Bank entity = _mapper.Map<BankRequest, QTTS01_Bank>(obj);
                entity.create_by = checkExists.create_by;
                entity.create_time = checkExists.create_time;
                entity.is_active = checkExists.is_active;

                await _bankRepository.Update(entity, obj.id);
                BankResponse result = _mapper.Map<QTTS01_Bank, BankResponse>(entity);

                return new ResponseService<BankResponse>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<BankResponse>(ex);
            }
        }

        public async Task<ResponseService<bool>> Delete(Guid id)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                Guid tenantId = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);

                QTTS01_Bank checkExists = await _bankRepository.GetSingle(x => x.id == id && x.tenant_id == tenantId);
                if (checkExists == null)
                {
                    return new ResponseService<bool>(Constants.BANK_NOT_FOUND).BadRequest(MessCodes.BANK_NOT_FOUND);
                }

                await _bankRepository.DeleteCustom(checkExists);

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
