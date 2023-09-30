using System;
using System.Collections.Generic;

namespace Common
{
    public class Constants
    {
        public static readonly int SERVICE_CODE = 1;
        public static readonly string CORE_SECRET_KEY_LCM = "bpZC9cqp54RqycnMQMUMunjnZ9uph/qY";
        public static readonly string CONSUL_AGENT_SERVICE_NAME = "CONSUL_AGENT_SERVICE_NAME";
        public static readonly string HOST_FABIO_SERVICE = "HOST_FABIO_SERVICE";
        public static readonly string CONF_CORE_SECRET_KEY_LCM = "CONF_CORE_SECRET_KEY_LCM";
        public static readonly string CONF_TOKEN_EXPIRATION_TIME = "TOKEN_EXPIRATION_TIME";
        public static readonly string CONF_TOKEN_EXPIRATION_TIME_BONUS = "TOKEN_EXPIRATION_TIME_BONUS";
        public static readonly string CONF_CROSS_ORIGIN = "CROSS_ORIGIN";
        public static readonly string CONF_KAFKA_GROUP_ID = "KAFKA_GROUP_ID";
        public static readonly string CONF_KAFKA_SESSION_TIME_OUT = "KAFKA_SESSION_TIME_OUT";
        public static readonly string CONF_ADDRESS_DISCOVERY_SERVICE = "ADDRESS_DISCOVERY_SERVICE";
        public static readonly string CONF_ADDRESS_SERVICE = "ADDRESS_SERVICE";
        public static readonly string CONF_PORT_SERVICE = "PORT_SERVICE";
        public static readonly string CONF_PROTOCOL_SERVICE = "PROTOCOL_SERVICE";
        public static readonly string CONF_SOURCE_FABIO_SERVICE = "SOURCE_FABIO_SERVICE";
        public static readonly string CONF_MAX_ERROR_MESS = "MAX_ERROR_MESS";
        public static readonly string CONF_SOURCE_SETTINGS_FABIO = "SOURCE_SETTINGS_FABIO";
        public static readonly string CONF_SOURCE_EMAIL_FABIO = "SOURCE_EMAIL_FABIO";
        public static readonly string CONF_HOST_FABIO_SERVICE = "HOST_FABIO_SERVICE";
        public static readonly string CONF_API_SECRET_KEY = "API_SECRET_KEY";
        public static readonly string CONF_SECRET_KEY = "SECRET_KEY";
        public static readonly string CONF_LINK_FORGOT_PASSWORD_IC = "LINK_FORGOT_PASSWORD_IC";
        public static readonly string CONF_SOURCE_USER_FABIO = "SOURCE_USER_FABIO";
        public static readonly string CONF_LINK_USER_QTTS = "LINK_USER_QTTS";
        public static readonly string CONF_STATE_SOURCE = "STATE_SOURCE";
        public static readonly string CONF_API_PUBLIC_KEY = "PUBLIC_KEY";
        public static readonly string CONF_DEFAULT_START_EXTENSION_RANGE = "DEFAULT_START_EXTENSION_RANGE";
        public static readonly string CONF_VIETNAM_INTERNATIONAL_CODE = "VIETNAM_INTERNATIONAL_CODE";
        public static readonly string CONF_AUTH_DOMAIN = "AUTH_DOMAIN";

        public enum OPERATOR_FILTER { EQ, CMP, GT, GTE, LT, LTE, NE, EXIST, AVG, FIRST, LAST, MAX, MIN, SUM }
        public enum TYPE_DATA_CAMPARE { STRING, DATE, DATE_TIME, INT, FLOAT, BOOL }

        public static readonly string GROUP_YEAR = "year";
        public static readonly string GROUP_MONTH = "month";
        public static readonly string GROUP_DAY = "day";
        public static readonly string GROUP_HOUR = "hour";
        public static readonly string GROUP_MINUTE = "minute";

        public static readonly string SERVICE_CONSULT_GOOD_HEALTH = "passing";
        public static readonly string STATE_SOURCE_PRODUCTION = "production";
        public static readonly string STATE_SOURCE_DEV = "dev";

        public static readonly string PROFILE_ADMIN = "Admin";
        public static readonly string ROOT = "root";
        public static readonly string ADMIN = "admin";
        public static readonly string SYSTEM = "system";

        #region key session store
        public static readonly string KEY_SESSION_TENANT_ID = "KEY_SESSION_TENANT_ID";
        public static readonly string KEY_SESSION_GROUP_ID = "KEY_SESSION_GROUP_ID";
        public static readonly string KEY_SESSION_EMAIL = "KEY_SESSION_EMAIL";
        public static readonly string KEY_SESSION_USER_ID = "KEY_SESSION_USER_ID";
        public static readonly string KEY_SESSION_TOKEN = "KEY_SESSION_TOKEN";
        public static readonly string KEY_SESSION_IS_ADMIN = "KEY_SESSION_IS_ADMIN";
        public static readonly string KEY_SESSION_IS_ROOT = "KEY_SESSION_IS_ROOT";
        #endregion

        #region root permission object
        public static readonly string[] ROOT_PERMISSIONS = { "Tenants", "Report Category", "Report Config", "Action Log", "Permission Object" };
        #endregion

        #region agent permission object
        public static readonly string[] AGENT_PERMISSIONS = { "Agent Group", "Agent Monitor", "Common Setting", "Dashboard View", "Dynamic Report", "Queue", "Queue Monitor", "Recording", "Resource Manager", "State List", "User Management", "Work Screen", "Skill" };
        #endregion

        #region module object
        public static readonly string[] MODULE_OBJECTS = { "User Management", "General Setting" };
        #endregion

        public static readonly string MAX_TIME_FOR_TYPE_WRONG_PASSWORD = "max_time_for_type_wrong_password";
        public static readonly string NUMBER_HISTORY_PASSWORD_VALIDATE = "number_history_password_validate";
        public static readonly string TIME_BLOCK_BY_TYPE_WRONG_PASSWORD = "time_block_by_type_wrong_password";

        #region path local server
        public static readonly string PATH_TEMPLATE_DESTINATION_MATCHES = "/destination-matches-template.txt";
        public static readonly string PATH_TEMPLATE_DESTINATION_NON_MATCHES = "/destination-non-matches-template.txt";
        public static readonly string PATH_RESOURCE_BY_TENANT = "/Resources/tenant_";
        public static readonly string PATH_FILE_CONFIG = "/FileConfigs";
        public static readonly string PATH_FOLDER_AVATAR = "/avatar";
        #endregion

        #region error message
        public static readonly string USER_IS_INVALID = "User is invalid!";
        public static readonly string USERNAME_OR_PASSWORD_IS_INCORECT = "Username or password is incorect.";
        public static readonly string USERNAME_IS_EXISTS = "Username has already existed.";
        public static readonly string PASSWORD_DOES_NOT_MATCH = "Password does not match.";
        public static readonly string EMPLOYEE_CREATION_LIMIT = "Employee creation limit.";
        public static readonly string USER_NOT_FOUND = "User not found.";
        public static readonly string HOTLINE_NUMBER_ALREADY_EXISTS = "Hotline number already exists.";
        public static readonly string ACCESS_DENIED = "Access denied.";
        public static readonly string FILE_NAME_ALREADY_EXISTS = "File name already exists.";
        public static readonly string FILE_NOT_FOUND = "File not found.";
        public static readonly string NAME_IS_ALREADY_EXISTS = "Name has already existed.";
        public static readonly string DATA_NOT_FOUND = "Data not found.";
        public static readonly string PLEASE_TRY_AGAIN = "Please try again.";
        public static readonly string UPDATE_SUCCESSFULLY = "Updated successfully.";
        public static readonly string CREATE_SUCCESSFULLY = "Created successfully.";
        public static readonly string DELETE_SUCCESSFULLY = "Deleted successfully.";
        public static readonly string DATA_INVALID = "Data invalid.";
        public static readonly string MODULE_NAME_IS_ALREADY_EXISTS = "Module name has already existed.";
        public static readonly string BANK_NAME_IS_ALREADY_EXISTS = "Bank name has already existed.";
        public static readonly string GROUP_NAME_IS_ALREADY_EXISTS = "Group name has already existed.";
        public static readonly string TENANT_NAME_IS_ALREADY_EXISTS = "Tenant name has already existed.";
        public static readonly string EMAIL_IS_ALREADY_EXISTS = "Email has already existed!!";
        public static readonly string OBJECT_NAME_HAS_ALREADY_EXISTED = "Object name has already existed.";
        public static readonly string BANK_NOT_FOUND = "Bank not found.";
        public static readonly string GROUP_NOT_FOUND = "Group not found.";
        public static readonly string TENANT_NOT_FOUND = "Tenant not found.";
        public static readonly string PERMISSION_OBJECT_HAS_ALREADY_EXISTED = "Permission object has already existed.";
        public static readonly string PROFILE_HAS_ALREADY_EXISTED = "Profile has already existed.";
        public static readonly string PERMISSION_OBJECT_NOT_FOUND = "Permission object not found.";
        public static readonly string PROFILE_NOT_FOUND = "Profile not found.";
        public static readonly string AGENT_IS_IN_USE = "This agent is currently in use and cannot be deleted.";
        public static readonly string ROLE_NOT_FOUND = "Role not found.";
        public static readonly string SYSTEM_DATA_CANT_UPDATE = "Cannot update this item.";
        public static readonly string SYSTEM_DATA_CANT_DELETE = "Cannot delete this item.";
        public static readonly string SERVER_IS_IN_USE = "This server is currently in use and cannot be deleted.";
        public static readonly string START_COMPONENT_IS_NOT_COMPLETED_YET = "Start component is not completed yet.";
        public static readonly string MAX_FILE_SIZE_IS_LARGER_THAN_SYSTEM_FILE_SIZE = "Max file size is larger than system file size.";
        public static readonly string ACCOUNT_LOCKED = "Account locked.";
        public static readonly string MODULE_NOT_FOUND = "Module not found.";
        public static readonly string DUPLICATE_PASSWORD = "Duplicate password.";
        public static readonly string UPDATE_PASSWORD_SUCCESSFULLY = "Update password successfully !!!";
        public static readonly string OLD_PASSWORD_IS_NOT_CORRECT = "Old password is not correct.";
        public static readonly string NEW_PASSWORD_CANNOT_SAME_OLD_PASSWORD = "The new password cannot be the same as the old password.";
        public static readonly string WEBHOOK_EVENT_KEY_IS_ALREADY_EXISTS = "Webhook event key has already existed.";
        public static readonly string WEBHOOK_EVENT_NAME_IS_ALREADY_EXISTS = "Webhook event name has already existed.";
        public static readonly string WEBHOOK_HEADER_KEY_IS_ALREADY_EXISTS = "Webhook header key has already existed.";
        public static readonly string WEBHOOK_SETTING_NOT_FOUND = "Webhook setting not found.";
        public static readonly string DUPLICATE_RECORDING_ID = "Duplicate recording id.";
        public static readonly string DUPLICATE_SKILL = "Duplicate skill.";
        public static readonly string ADMIN_PROFILE_CANNOT_BE_DELETED = "The admin profile cannot be deleted.";
        public static readonly string ADMIN_PROFILE_CANNOT_BE_UPDATED = "The admin profile cannot be updated.";
        public static readonly string MODEL_STATE_INVALID = "Model state invalid !";
        public static readonly string AUTHEN_FAILED = "Authen failed.";
        public static readonly string TENANT_IS_DEACTIVE = "Tenant is deactive !";
        public static readonly string EXPIRED_TRIAL_PERIOD = "Expired trial period !";
        public static readonly string PLEASE_CHOOSE_EXCEL_FILE = "Please choose excel file.";
        public static readonly string REPORT_TO_NOT_FOUND = "Report to not found !";
        public static readonly string THIS_USER_IS_IN_MAPPING = "This user is in mapping.";
        public static readonly string YOU_DONT_HAVE_PERMISSION_TO_DO_THIS = "You don't have permission to do this.";
        #endregion

        #region hashtype
        public static readonly string HASH_SHA512 = "SHA512";
        public static readonly string HASH_SHA384 = "SHA384";
        public static readonly string HASH_SHA256 = "SHA256";
        #endregion

        #region author enum
        public enum AUTHOR { TOKEN, SECRET_KEY, TOKEN_OR_KEY };
        #endregion

        #region log type
        public const string LOG_TYPE_USER_LOGIN = "User Login";
        public const string LOG_TYPE_USER_LOGOUT = "User Logout";
        public const string LOG_TYPE_CREATE = "Create";
        public const string LOG_TYPE_UPDATE = "Update";
        public const string LOG_TYPE_DELETE = "Delete";
        public const string LOG_TYPE_ACCESS = "Access";

        #endregion

        #region log message
        public const string USER_LOGIN_SUCCESSFULLY = "User Login Successfully.";
        public const string USER_LOGIN_FAILED = "User Login Failed.";
        public const string USER_LOGOUT_SUCCESSFULLY = "User Logout Successfully.";
        public const string USER_LOGOUT_FAILED = "User Logout Failed.";
        public const string CREATED_SUCCESSFULLY = "Created successfully.";
        public const string UPDATED_SUCCESSFULLY = "Updated successfully.";
        public const string DELETED_SUCCESSFULLY = "Deleted successfully.";
        public const string CREATED_FAILED = "Created failed.";
        public const string UPDATED_FAILED = "Updated failed.";
        public const string DELETED_FAILED = "Deleted failed.";
        public const string UPDATED_IS_ACTIVE_SUCCESSFULLY = "Updated is active successfully.";
        public const string UPDATED_IS_ACTIVE_FAILED = "Updated is active failed.";
        public const string CHANGE_PASSWORD_SUCCESSFULLY = "Change password successfully.";
        public const string CHANGE_PASSWORD_FAILED = "Change password failed.";
        public const string ADD_USER_TO_PROFILE_SUCCESSFULLY = "Add user to profile successfully.";
        public const string ADD_USER_TO_PROFILE_FAILED = "Add user to profile failed.";
        public const string UPDATED_PERMISSION_ON_PROFILE_SUCCESSFULLY = "Updated permissions on the profile successfully.";
        public const string UPDATED_PERMISSION_ON_PROFILE_FAILED = "Updated permissions on the profile failed.";
        public const string REMOVE_USER_FROM_PROFILE_SUCCESSFULLY = "Remove users from the profile successfully.";
        public const string REMOVE_USER_FROM_PROFILE_FAILED = "Remove users from the profile failed.";
        public const string IMPORT_DATA_FAILED = "Import data failed.";
        public const string UPLOADED_FILE_SUCCESSFULLY = "Uploaded file successfully.";
        public const string UPLOADED_FILE_FAILED = "Uploaded file failed.";
        public const string DELETED_FILE_SUCCESSFULLY = "Deleted file successfully.";
        public const string DELETED_FILE_FAILED = "Deleted file failed.";

        #endregion

        #region hashtag redis
        public static readonly string HASHTAG_CONNECTION = "connection";
        #endregion
        #region list colors to generate default avatar
        public static List<string> COLOR_CODES = new List<string> {
            "#EEAD0E", "#8bbf61", "#DC143C", "#CD6889", "#8B8386", "#800080", "#9932CC", "#009ACD", "#00CED1", "#03A89E",
            "#00C78C", "#00CD66", "#66CD00", "#EEB422", "#FF8C00", "#EE4000", "#388E8E", "#8E8E38", "#7171C6" };
        #endregion
        #region extension file
        public static readonly string EXTENSION_FILE_AVATAR = ".jpg";
        #endregion

        #region excel template 
        //public static readonly string CONTACT_PRIORITY_LIST_TEMPLATE = "File/vip_list_template.xlsx";
        #endregion
    }
}
