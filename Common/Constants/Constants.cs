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
        public static readonly string CONF_KAFKA_BOOSTRAP_SERVER = "KAFKA_BOOSTRAP_SERVER";
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
        public static readonly string CONF_LINK_KONG_VOICE_GATEWAY_ACTION = "LINK_KONG_VOICE_GATEWAY_ACTION";
        public static readonly string CONF_LINK_API_BCC_ASSIGN_DATA = "LINK_API_ASSIGN_DATA";
        public static readonly string CONF_LINK_SOURCE_DATASTORAGE_FABIO = "SOURCE_DATASTORE_FABIO";
        public static readonly string CONF_URL_API_HANDLE_ERROR_INBOUND_CALL = "URL_API_HANDLE_ERROR_INBOUNDCALL";
        public static readonly string CONF_URL_API_ROUTING_AGENT_AVAILABLE = "URL_API_ROUTING_AGENT_AVAILABLE";
        public static readonly string CONF_URL_API_UPDATE_AGENT_HANGUP_STATUS = "URL_API_UPDATE_AGENT_HANGUP_STATUS";
        public static readonly string CONF_STATE_SOURCE = "STATE_SOURCE";
        public static readonly string CONF_API_PUBLIC_KEY = "PUBLIC_KEY";
        public static readonly string CONF_LIMIT_EXTENSION_RANGE_BY_TENANT = "LIMIT_EXTENSION_RANGE_BY_TENANT";
        public static readonly string CONF_LIMIT_TENANT_PER_ASTERISK_SERVER = "LIMIT_TENANT_PER_ASTERISK_SERVER";
        public static readonly string CONF_DEFAULT_START_EXTENSION_RANGE = "DEFAULT_START_EXTENSION_RANGE";
        public static readonly string CONF_VIETNAM_INTERNATIONAL_CODE = "VIETNAM_INTERNATIONAL_CODE";
        public static readonly string CONF_VIETNAM_INTERNATIONAL_CODE_WITHOUT_PLUS = "VIETNAM_INTERNATIONAL_CODE_WITHOUT_PLUS";
        public static readonly string CONF_AUTH_DOMAIN = "AUTH_DOMAIN";
        public static readonly string CONF_LINK_SOURCE_WEB_HOOK_FABIO = "SOURCE_WEB_HOOK_FABIO";
        public static readonly string CONF_URL_API_CHECK_PHONE_NUMBER_IS_VIP = "URL_API_CHECK_PHONE_NUMBER_IS_VIP";
        public static readonly string CONF_URL_API_CHECK_PHONE_NUMBER_IN_BLACK_LIST = "URL_API_CHECK_PHONE_NUMBER_IN_BLACK_LIST";
        public static readonly string CONF_TOKEN_EXPIRATION_TIME_3RD_BY_MINUTE = "TOKEN_EXPIRATION_TIME_3RD_BY_MINUTE";

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
        public static readonly string KEY_SESSION_EMAIL = "KEY_SESSION_EMAIL";
        public static readonly string KEY_SESSION_USER_ID = "KEY_SESSION_USER_ID";
        public static readonly string KEY_SESSION_TOKEN = "KEY_SESSION_TOKEN";
        public static readonly string KEY_SESSION_ROLE_ID = "KEY_SESSION_ROLE_ID";
        public static readonly string KEY_SESSION_IS_ADMIN = "KEY_SESSION_IS_ADMIN";
        public static readonly string KEY_SESSION_IS_ROOT = "KEY_SESSION_IS_ROOT";
        public static readonly string KEY_SESSION_IS_SUPERVISOR = "KEY_SESSION_IS_SUPERVISOR";
        public static readonly string KEY_SESSION_IS_AGENT = "KEY_SESSION_IS_AGENT";
        public static readonly string KEY_SESSION_ASTERISK_ID = "KEY_SESSION_ASTERISK_ID";
        public static readonly string KEY_SESSION_EXTENSION_NUMBER = "KEY_SESSION_EXTENSION_NUMBER";
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

        #region webhook event
        public static readonly string WEBHOOK_EVENT_DELETE_USER = "delete_user";
        public static readonly string WEBHOOK_EVENT_CREATE_NEW_USER = "create_new_user";
        public static readonly string WEBHOOK_EVENT_UPDATE_USER = "update_user";
        public static readonly string WEBHOOK_EVENT_AGENT_CHANGE_STATE = "agent_change_state";
        public static readonly string WEBHOOK_EVENT_AGENT_LOGIN = "agent_login";
        public static readonly string WEBHOOK_EVENT_AGENT_LOGOUT = "agent_logout";
        #endregion

        public static readonly string MAX_TIME_FOR_TYPE_WRONG_PASSWORD = "max_time_for_type_wrong_password";
        public static readonly string NUMBER_HISTORY_PASSWORD_VALIDATE = "number_history_password_validate";
        public static readonly string TIME_BLOCK_BY_TYPE_WRONG_PASSWORD = "time_block_by_type_wrong_password";

        #region path in asterisk server
        // Path in server voice portal 
        public static readonly string PATH_FOLDER_TENANT_VP = "/etc/asterisk/tenant_";
        public static readonly string REMOTE_PATH_ASTERISK_VP = "/etc/asterisk";
        public static readonly string PATH_SYSTEM_RECORDING_ASTERISK_VP = "/var/lib/asterisk/sounds/en/custom/";
        public static readonly string PATH_SYSTEM_RECORDING_ASTERISK_BY_TENANT_VP = "/var/lib/asterisk/sounds/en/custom/tenant_";
        public static readonly string PATH_FOLDER_BASEBS_ADDITIONAL_VP = "/etc/asterisk/basebs_additional";
        public static readonly string PATH_FILE_SIP_CUSTOM_VP = "/etc/asterisk/sip_custom.conf";
        public static readonly string PATH_FILE_EXTENTION_CUSTOM_VP = "/etc/asterisk/extensions_custom.conf";
        public static readonly string PATH_FOLDER_BASEBS_BACKUP_VP = "/etc/asterisk/basebs_backup";
        public static readonly string PATH_FOLDER_BASEBS_DEFAULT_SYSTEM_RECORDING_VP = "/var/lib/asterisk/sounds/en/custom/basebs_default_system_recording";
        public static readonly string PATH_FOLDER_ASTERISK_API = "/var/www/html/api";
        public static readonly string PATH_CALL_ACTION_API = "/var/www/html/api/call_action.php";

        #endregion

        #region path local server
        public static readonly string PATH_EXTENSION_SIP_TEMPLATE = "/wwwroot/template-asterisk/extension/sip-template.txt";
        public static readonly string PATH_TCD_DESTINATION_MATCHES_TEMPLATE = "/wwwroot/template-asterisk/timecondition/destination-matches-template.txt";
        public static readonly string PATH_TCD_DESTINATION_NON_MATCHES_TEMPLATE = "/wwwroot/template-asterisk/timecondition/destination-non-matches-template.txt";
        public static readonly string PATH_TCD_HEADER_TEMPLATE = "/wwwroot/template-asterisk/timecondition/header-template.txt";
        public static readonly string PATH_TCD_TIMEGROUP_TEMPLATE = "/wwwroot/template-asterisk/timecondition/timegroup-template.txt";
        public static readonly string PATH_EXTENSION_PUT_DATA_TEMPLATE = "/wwwroot/template-asterisk/extension/put-data-template.txt";
        public static readonly string PATH_EXTENSION_TEMPLATE = "/wwwroot/template-asterisk/extension/extension-template.txt";
        public static readonly string PATH_RELOAD_SERVER_TEMPLATE = "/wwwroot/template-asterisk/reload-server/reload-server-template.txt";
        public static readonly string PATH_HOTLINE_TEMPLATE = "/wwwroot/template-asterisk/hotline/hotline-template.txt";
        public static readonly string PATH_IVR_HEADER_TEMPLATE = "/wwwroot/template-asterisk/ivr/header-template.txt";
        public static readonly string PATH_IVR_DIGIT_TEMPLATE = "/wwwroot/template-asterisk/ivr/digit-template.txt";
        public static readonly string PATH_IVR_INVALID_AND_TIMEOUT_TEMPLATE = "/wwwroot/template-asterisk/ivr/invalid-and-timeout-template.txt";
        public static readonly string PATH_OUTBOUND_ALLROUTES_TEMPLATE = "/wwwroot/template-asterisk/outbound/outbound-allroutes-template.txt";
        public static readonly string PATH_OUTBOUND_HOTLINE_TEMPLATE = "/wwwroot/template-asterisk/outbound/outbound-hotline-template.txt";
        public static readonly string PATH_OUTBOUND_MOBILE_TEMPLATE = "/wwwroot/template-asterisk/outbound/outbound-mobile-template.txt";
        public static readonly string PATH_OUTBOUND_HOMEPHONE_TEMPLATE = "/wwwroot/template-asterisk/outbound/outbound-homephone-template.txt";
        public static readonly string PATH_TRUNK_EXTENSION_TEMPLATE = "/wwwroot/template-asterisk/trunk/trunk-extension-template.txt";
        public static readonly string PATH_TRUNK_SIP_TEMPLATE = "/wwwroot/template-asterisk/trunk/trunk-sip-template.txt";
        public static readonly string PATH_TRUNK_MACRO_DIALOUT_TEMPLATE = "/wwwroot/template-asterisk/trunk/trunk-macro-dialout-template.txt";
        public static readonly string PATH_TEMPLATE = "/wwwroot/template-asterisk";
        public static readonly string PATH_TEMPLATE_TIMECONDITION = "/wwwroot/template-asterisk/timecondition";
        public static readonly string PATH_TEMPLATE_CHECK_VIP = "/wwwroot/template-asterisk/check-vip";
        public static readonly string PATH_TEMPLATE_DESTINATION_MATCHES = "/destination-matches-template.txt";
        public static readonly string PATH_TEMPLATE_DESTINATION_NON_MATCHES = "/destination-non-matches-template.txt";
        public static readonly string PATH_TEMPLATE_IVR = "/wwwroot/template-asterisk/ivr";
        public static readonly string PATH_TEMPLATE_OUTBOUND = "/wwwroot/template-asterisk/outbound";
        public static readonly string PATH_TEMPLATE_ANNOUNCEMENT = "/wwwroot/template-asterisk/announcement";
        public static readonly string PATH_ROUTING_CUSTOM_TEMPLATE = "/wwwroot/template-asterisk/routing/routing-custom-template.txt";
        public static readonly string PATH_TEMPLATE_ROUTING = "/wwwroot/template-asterisk/routing";
        public static readonly string PATH_TEMPLATE_ROUTING_CUSTOM = "/routing-custom-template.txt";
        public static readonly string PATH_TEMPLATE_CUSTOM_DESTINATION = "/wwwroot/template-asterisk/custom-destination/custom-destination-template.txt";
        public static readonly string PATH_TEMPLATE_HEADER_IVR = "/header-template.txt";
        public static readonly string PATH_ANNOUNCEMENT_NOANSWER_TEMPLATE = "/wwwroot/template-asterisk/announcement/noanswer-template.txt";
        public static readonly string PATH_ANNOUNCEMENT_NM_TEMPLATE = "/wwwroot/template-asterisk/announcement/nm-template.txt";
        public static readonly string PATH_CHECK_VIP_TEMPLATE = "/wwwroot/template-asterisk/check-vip/checkvip-template.txt";
        public static readonly string PATH_CHECK_BLACK_LIST_TEMPLATE = "/wwwroot/template-asterisk/check-black-list/checkblacklist-template.txt";
        public static readonly string PATH_TEMPLATE_DIGITS = "/digit-template.txt";
        public static readonly string PATH_TEMPLATE_INVALID_TIMEOUT = "/invalid-and-timeout-template.txt";
        public static readonly string PATH_TEMPLATE_HEADER_TIMECONDITION = "/header-template.txt";
        public static readonly string PATH_TEMPLATE_TOGGLES = "/toggles-template.txt";
        public static readonly string PATH_TEMPLATE_TIME_GROUP = "/timegroup-template.txt";
        public static readonly string PATH_RESOURCE_BY_TENANT = "/Resources/tenant_";
        public static readonly string PATH_RESOURCE_TRUNK = "/Resources/trunk";
        public static readonly string PATH_SYSTEM_RECORDING = "/SystemRecordings";
        public static readonly string PATH_FILE_CONFIG = "/FileConfigs";
        public static readonly string PATH_FOLDER_CONFIG_TIMECONDITION = "/FileConfigs/TimeConditions";
        public static readonly string PATH_FOLDER_CONFIG_HOTLINE = "/FileConfigs/Hotlines";
        public static readonly string PATH_FOLDER_CONFIG_IVR = "/FileConfigs/IVRs";
        public static readonly string PATH_FOLDER_CONFIG_EXTENSION = "/FileConfigs/Extensions";
        public static readonly string PATH_FOLDER_CONFIG_ANNOUNCEMENT = "/FileConfigs/Announcements";
        public static readonly string PATH_FOLDER_CONFIG_QUEUE = "/FileConfigs/Queues";
        public static readonly string PATH_FOLDER_CONFIG_CHECK_VIP = "/FileConfigs/CheckVips";
        public static readonly string PATH_FOLDER_CONFIG_CHECK_BLACK_LIST = "/FileConfigs/CheckBlackList";
        public static readonly string PATH_FOLDER_CONFIG_AGENT_EXTENSION_COMPONENT = "/FileConfigs/AgentExtensionComponents";
        public static readonly string PATH_FOLDER_CONFIG_OUTBOUND = "/FileConfigs/Outbounds";
        public static readonly string PATH_FOLDER_CONFIG_TRUNK = "/FileConfigs/Trunks";
        public static readonly string PATH_FOLDER_CONFIG_CALL_ACTION = "/Resources/FileConfigs/CallActions";
        public static readonly string PATH_FOLDER_CONFIG_CALLFLOW = "/callflow_";
        public static readonly string PATH_FOLDER_DEFAULT_SYSTEM_RECORDING = "/Resources/DefaultSystemRecording";
        public static readonly string PATH_FOLDER_AVATAR = "/avatar";
        public static readonly string PATH_CALL_ACTION_API_TEMPLATE = "/wwwroot/template-asterisk/call-action/call_action.txt";
        public static readonly string PATH_AGENT_EXTENSION_COMPONENT_TEMPLATE = "/wwwroot/template-asterisk/agent-extension-component/agent-extension-template.txt";
        public static readonly string PATH_FOLDER_CONFIG_MOBILE_NUMBER_COMPONENT = "/FileConfigs/MobileNumberComponents";
        public static readonly string PATH_MOBILE_NUMBER_COMPONENT_TEMPLATE = "/wwwroot/template-asterisk/mobile-number-component/mobile-number-template.txt";
        #endregion

        #region error message
        public static readonly string ASTERISK_ERROR = "Asterisk error.";
        public static readonly string USER_IS_INVALID = "User is invalid!";
        public static readonly string USERNAME_OR_PASSWORD_IS_INCORECT = "Username or password is incorect.";
        public static readonly string USERNAME_OR_EXTENSION_NUMBER_IS_EXISTS = "Username or extension number has already existed.";
        public static readonly string PASSWORD_DOES_NOT_MATCH = "Password does not match.";
        public static readonly string EMPLOYEE_CREATION_LIMIT = "Employee creation limit.";
        public static readonly string USER_NOT_FOUND = "User not found.";
        public static readonly string HOTLINE_NUMBER_ALREADY_EXISTS = "Hotline number already exists.";
        public static readonly string IMPORT_HOTLINE_NUMBER_IS_ALREADY_EXISTS = "Hotline number {0} has already existed.";
        public static readonly string IMPORT_HOTLINE_LIBRARY_IS_DATA_INVALID = "Uploaded file is in the wrong format.";
        public static readonly string HOTLINE_IS_USED_IN_ANOTHER_CALLFLOW = "Hotline number is used in another callflow.";
        public static readonly string HOTLINE_CANNOT_DELETE = "This hotline is currently in use and cannot be deleted.";
        public static readonly string HOTLINE_NOT_FOUND = "Hotline not found.";
        public static readonly string ACCESS_DENIED = "Access denied.";
        public static readonly string FILE_NAME_ALREADY_EXISTS = "File name already exists.";
        public static readonly string FILE_NOT_FOUND = "File not found.";
        public static readonly string PLEASE_CHOOSE_MP3_OR_WAV_FILE = "Please choose a file in MP3 or WAV format!.";
        public static readonly string SYSTEM_RECORDING_NOT_FOUND = "System recording not found.";
        public static readonly string SYSTEM_RECORDING_IS_IN_USE = "Recording is using in another callflow.";
        public static readonly string SYSTEM_RECORDING_CANNOT_DELETE = "This recording is currently in use and cannot be deleted.";
        public static readonly string NAME_IS_ALREADY_EXISTS = "Name has already existed.";
        public static readonly string CALLFLOW_NOT_FOUND = "Callflow not found.";
        public static readonly string CALLFLOW_NOT_FOUND_HOTLINE = "Component start not found in this callflow.";
        public static readonly string TIME_GROUP_NOT_FOUND = "Time group not found.";
        public static readonly string TIME_GROUP_IS_IN_USE = "This time group is currently in use and cannot be deleted.";
        public static readonly string DATA_NOT_FOUND = "Data not found.";
        public static readonly string CANT_CONNECT_TO_ASTERISK = "Can't connect to asterisk server.";
        public static readonly string TCD_IS_NOT_COMPLETED_YET = "Timecondition component is not completed yet.";
        public static readonly string IVR_IS_NOT_COMPLETED_YET = "IVR component is not completed yet.";
        public static readonly string HOTLINE_IS_NOT_COMPLETED_YET = "Hotline is not completed yet.";
        public static readonly string ANNOUNCEMENT_IS_NOT_COMPLETED_YET = "Announcement component is not completed yet.";
        public static readonly string QUEUE_IS_NOT_COMPLETED_YET = "Queue component is not completed yet.";
        public static readonly string PLEASE_TRY_AGAIN = "Please try again.";
        public static readonly string CANNOT_CONNECT_TO_VOICE_PORTAL_SERVER = "Cannot connect to Voice Portal server.";
        public static readonly string CANNOT_CONNECT_TO_PBX_SERVER = "Cannot connect to Pbx server.";
        public static readonly string COMPONENT_NOT_FOUND = "Component not found.";
        public static readonly string DUPLICATE_ACTION = "Duplicate action";
        public static readonly string AGENTGROUP_NAME_HAS_ALREADY_EXISTED = "Agent group name has already existed.";
        public static readonly string AGENT_IS_ALREADY_IN_GROUP = "Agent has already existed in this group.";
        public static readonly string AGENTGROUP_NOT_FOUND = "Agent group not found.";
        public static readonly string AGENTGROUP_IS_IN_USE = "Agent group is in use.";
        public static readonly string USER_NOT_FOUND_IN_GROUP = "User not found in this group.";
        public static readonly string FILE_SOUND_NOT_FOUND = "File sound not found.";
        public static readonly string UPDATE_SUCCESSFULLY = "Updated successfully.";
        public static readonly string CREATE_SUCCESSFULLY = "Created successfully.";
        public static readonly string DELETE_SUCCESSFULLY = "Deleted successfully.";
        public static readonly string SKILL_NAME_HAS_ALREADY_EXISTED = "Skill name has already existed.";
        public static readonly string SKILL_NOT_FOUND = "Skill not found.";
        public static readonly string SKILL_IS_IN_USE = "Skill is in use.";
        public static readonly string ALIAS_OUT_ID_IS_IN_USE = "Alias out id is in use another component.";
        public static readonly string ALIAS_OUT_NOT_FOUND = "Alias out not found.";
        public static readonly string SKILL_OR_USER_NOT_FOUND = "Skill or user not found.";
        public static readonly string DATA_INVALID = "Data invalid.";
        public static readonly string AGENT_GROUP_OR_USER_NOT_FOUND = "Agent group or user not found.";
        public static readonly string MODULE_NAME_IS_ALREADY_EXISTS = "Module name has already existed.";
        public static readonly string EMAIL_IS_ALREADY_EXISTS = "Email has already existed!!";
        public static readonly string ASTERISK_SERVER_IS_FULL = "Asterisk server is full.";
        public static readonly string ASTERISK_SERVER_NOT_FOUND = "Server not found.";
        public static readonly string EXTENSION_RANGE_NOT_FOUND = "Extension range not found.";
        public static readonly string EXTENSION_NUMBER_IS_MAX = "Extension number is max.";
        public static readonly string OBJECT_NAME_HAS_ALREADY_EXISTED = "Object name has already existed.";
        public static readonly string AGENT_NOT_EXISTS_OR_IS_ALREADY_IN_SKILL = "Agent is not exists or has already existed in skill.";
        public static readonly string QUEUE_NAME_HAS_ALREADY_EXISTED = "Queue name has already existed.";
        public static readonly string ONLY_CHOOSE_GROUP_OR_STEP = "Can only choose agent group or step condition.";
        public static readonly string QUEUE_NOT_FOUND = "Queue not found in this tenant.";
        public static readonly string QUEUE_IS_IN_USE = "Queue is in use, cannot be deleted!";
        public static readonly string QUEUE_STEP_NOT_FOUND = "Queue step not found.";
        public static readonly string QUEUE_STEP_CONDITION_NOT_FOUND = "Queue step condition not found.";
        public static readonly string AGENT_NOT_FOUND = "Agent not found in this tenant.";
        public static readonly string ATTRIBUTE_NOT_FOUND = "Attribute not found.";
        public static readonly string CANT_CHOOSE_AGENT_ORDER = "Can't choose this agent order for agent group.";
        public static readonly string DUPLICATE_ACTION_START = "Duplicate action component hotline.";
        public static readonly string DUPLICATE_ACTION_TIMECONDITION_MATCHES = "Duplicate action timecondition matches.";
        public static readonly string DUPLICATE_ACTION_TIMECONDITION_NON_MATCHES = "Duplicate action timecondition non matches.";
        public static readonly string DUPLICATE_ACTION_IVR_INVALID = "Duplicate action IVR invalid.";
        public static readonly string DUPLICATE_ACTION_IVR_TIMEOUT = "Duplicate action IVR timeout.";
        public static readonly string DUPLICATE_ACTION_ANNOUNCEMENT_AFTER_PLAYBACK = "Duplicate action announcement after playback.";
        public static readonly string TENANT_NOT_FOUND = "Tenant not found.";
        public static readonly string OUTBOUND_ROUTE_NOT_FOUND = "Outbound route not found.";
        public static readonly string OUTBOUND_NUMBER_HAS_ALREADY_EXISTED = "Outbound number has already existed.";
        public static readonly string FILE_DONT_SAME_FORMAT = "Can't concatenate WAV Files that don't share the same format.";
        public static readonly string PROFILE_NOT_FOUND = "Profile not found.";
        public static readonly string AGENT_IS_IN_USE = "This agent is currently in use and cannot be deleted.";
        public static readonly string ROLE_NOT_FOUND = "Role not found.";
        public static readonly string TRUNK_NOT_FOUND = "Trunk not found.";
        public static readonly string TRUNK_IP_IS_ALREADY_EXISTS = "Trunk ip has already existed.";
        public static readonly string STATE_NAME_IS_ALREADY_EXISTS = "State name has already existed.";
        public static readonly string ROLE_NAME_HAS_ALREADY_EXISTED = "Role name has already existed.";
        public static readonly string STATE_NOT_FOUND = "State not found.";
        public static readonly string SYSTEM_DATA_CANT_UPDATE = "Cannot update this item.";
        public static readonly string SYSTEM_DATA_CANT_DELETE = "Cannot delete this item.";
        public static readonly string SERVER_IS_IN_USE = "This server is currently in use and cannot be deleted.";
        public static readonly string TRUNK_IS_IN_USE = "This trunk is currently in use and cannot be deleted.";
        public static readonly string START_COMPONENT_IS_NOT_COMPLETED_YET = "Start component is not completed yet.";
        public static readonly string TCD_COMPONENT_MATCHES_IS_NOT_COMPLETED_YET = "Matches destination is not completed yet.";
        public static readonly string TCD_COMPONENT_NON_MATCHES_IS_NOT_COMPLETED_YET = "Non-matches destination is not completed yet.";
        public static readonly string IVR_COMPONENT_INVALID_IS_NOT_COMPLETED_YET = "Invalid destination is not completed yet.";
        public static readonly string IVR_COMPONENT_TIMEOUT_IS_NOT_COMPLETED_YET = "Timeout destination is not completed yet.";
        public static readonly string IVR_COMPONENT_RECORDING_IS_NOT_COMPLETED_YET = "Recording is not completed yet.";
        public static readonly string IVR_COMPONENT_DIGIT_IS_NOT_COMPLETED_YET = "Digit destination is not completed yet.";
        public static readonly string ANNOUNCEMENT_COMPONENT_RECORDING_IS_NOT_COMPLETED_YET = "Recording is not completed yet.";
        public static readonly string ANNOUNCEMENT_COMPONENT_AFTER_PLAYBACK_IS_NOT_COMPLETED_YET = "After playback destination is not completed yet.";
        public static readonly string QUEUE_COMPONENT_QUEUE_FAIL_OVER_IS_NOT_COMPLETED_YET = "Fail over destination is not completed yet.";
        public static readonly string EXTENSION_COMPONENT_IS_NOT_COMPLETED_YET = "Please choose agent.";
        public static readonly string SETTING_KEY_ALREADY_EXISTS = "Setting key has already existed.";
        public static readonly string STATE_KEY_ALREADY_EXISTS = "State key has already existed.";
        public static readonly string COMMON_SETTING_NOT_FOUND = "Common setting not found.";
        public static readonly string MAX_FILE_SIZE_IS_LARGER_THAN_SYSTEM_FILE_SIZE = "Max file size is larger than system file size.";
        public static readonly string WORK_SCREEN_NAME_HAS_ALREADY_EXISTED = "Work screen name has already existed.";
        public static readonly string WORK_SCREEN_NOT_FOUND = "Work screen not found.";
        public static readonly string ASSIGN_DATA_FAILED = "Assign data failed.";
        public static readonly string REMOVE_DATA_FAILED = "Remove data failed.";
        public static readonly string UPDATE_AGENT_IN_QUEUE_FAILED = "Remove data failed.";
        public static readonly string ACCOUNT_LOCKED = "Account locked.";
        public static readonly string COMPONENT_IS_NOT_COMPLETED_YET = "Component is not completed yet.";
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
        public static readonly string WEIGHT_VALUE_CANNOT_BE_LESS_THAN_0 = "Weight value cannot be less than 0.";
        public static readonly string START_COMPONENT_NOT_FOUND = "Start component not found.";
        public static readonly string DUPLICATE_POSITION = "Duplicate position.";
        public static readonly string AUTHEN_FAILED = "Authen failed.";
        public static readonly string THIS_ROLE_CANNOT_BE_DELETED = "This role cannot be deleted.";
        public static readonly string THIS_ROLE_CANNOT_BE_TRANSFERED = "This role cannot be transfered.";
        public static readonly string TENANT_IS_DEACTIVE = "Tenant is deactive !";
        public static readonly string EXPIRED_TRIAL_PERIOD = "Expired trial period !";
        public static readonly string PLEASE_CHOOSE_EXCEL_FILE = "Please choose excel file.";
        public static readonly string CANT_CONNECT_TO_THIRD_PARTY = "Can't connect to third party.";
        public static readonly string INTEGRATION_IS_ALREADY_EXISTS = "Integration config has already existed.";
        public static readonly string GET_TOKEN_INTEGRATION_IS_FAILED = "Data Integration invalid !";
        public static readonly string GROUP_NAME_IS_ALREADY_EXISTS = "Group name has already existed.";
        public static readonly string CONFIG_NOT_FOUND = "Config group not found.";
        public static readonly string CONTACT_CONFIG_IS_IN_USE = "This config is currently in use and cannot be deleted";
        public static readonly string REPORT_TO_NOT_FOUND = "Report to not found !";
        public static readonly string CANNOT_SELECT_ROLE_DIRECTOR = "Cannot select role director.";
        public static readonly string INVALID_REPORT_TO = "Invalid report to !";
        public static readonly string USER_REPORT_TO_IS_INVALID = "User report to is invalid !";
        public static readonly string CUSTOMER_NOT_FOUND_IN_THIS_LIST = "Customer not found in this list !";
        public static readonly string CHECK_VIP_COMPONENT_IS_NORMAL_IS_NOT_COMPLETED_YET = "Is normal destination is not completed yet.";
        public static readonly string CHECK_VIP_COMPONENT_IS_VIP_IS_NOT_COMPLETED_YET = "Is vip destination is not completed yet.";
        public static readonly string THIS_USER_CANNOT_BE_DELETED = "This user cannot be deleted.";
        public static readonly string SYNC_DATA_THIRD_PART_FAILED = "Sync-data failed.";
        public static readonly string ONLY_CHOOSE_ONE_START_COMPONENT = "Only choose one start component";
        public static readonly string ALIAS_IN_IS_NOT_COMPLETED_YET = "Alias in is not completed yet";
        public static readonly string ALIAS_OUT_IS_NEVER_USED = "Component alias out is created but its is never used";
        public static readonly string THE_ALIAS_OUT_OF_THIS_COMPONENT_NOT_FOUND = "The alias out of this component could not be found";
        public static readonly string TRIAL_CUSTOMER_CANNOT_CREATE_MULTI_HOTLINE = "Trial customers cannot be created with multiple hotline numbers.";
        public static readonly string DUPLICATE_ACTION_CHECK_VIP_IS_VIP = "Duplicate action check vip is vip";
        public static readonly string DUPLICATE_ACTION_CHECK_VIP_IS_NORMAL = "Duplicate action check vip is normal";
        public static readonly string DUPLICATE_ACTION_ALIAS_IN_TO = "Duplicate action alias in to";
        public static readonly string ONLY_ROOT_USER_CAN_DO_THIS = "Only root users can do this.";
        public static readonly string PERMISSION_OBJECT_HAS_ALREADY_EXISTED = "Permission object has already existed.";
        public static readonly string PROFILE_HAS_ALREADY_EXISTED = "Profile has already existed.";
        public static readonly string PERMISSION_OBJECT_NOT_FOUND = "Permission object not found.";
        public static readonly string ADMINISTRATOR_CANNOT_BE_REMOVED_ADMIN_PROFILE = "Administrators cannot be removed from the admin profile.";
        public static readonly string PHONRE_NUMBER_DO_NOT_CALL_NOT_FOUND = "The phone number already exists, please update the expiration date.";
        public static readonly string PHONRE_NUMBER_DO_NOT_CALL_IS_ALREADY_EXISTS = "Phone number has already existed.";
        public static readonly string IMPORT_PHONRE_NUMBER_DO_NOT_CALL_IS_DATA_INVALID = "The uploaded file is in the wrong format!";
        public static readonly string IMPORT_PHONRE_NUMBER_DO_NOT_CALL_IS_ALREADY_EXISTS = "Phone number {0} has already existed.";
        public static readonly string EXPIRE_TIME_PHONRE_NUMBER_DO_NOT_CALL_IS_ATA_INVALID = "The expiration time must be much more than today.";
        public static readonly string CHECK_PHONRE_NUMBER_DO_NOT_CALL = "The phone number is in Do Not Call mode.";
        public static readonly string MOBILE_NUMBER_COMPONENT_IS_NOT_COMPLETED_YET = "Please input mobile number.";
        public static readonly string PROVIDER_SERVER_NOT_FOUND = "Provider server not found !!!!";
        public static readonly string NODE_MOBILE_CALL_FAIL_IS_NOT_COMPLETED_YET = "Node mobile call fail is not completed yet.";
        public static readonly string DUPLICATE_ACTION_CHECK_BLACK_LIST_IS_BLACK = "Duplicate action check black list is black.";
        public static readonly string DUPLICATE_ACTION_CHECK_BLACK_LIST_IS_NORMAL = "Duplicate action check black list is normal.";
        public static readonly string CHECK_BLACK_LIST_COMPONENT_IS_NORMAL_IS_NOT_COMPLETED_YET = "Is normal destination is not completed yet.";
        public static readonly string CHECK_BLACK_LIST_COMPONENT_IS_BLACK_LIST_IS_NOT_COMPLETED_YET = "Is black list destination is not completed yet.";
        public static readonly string CUSTOMER_NOT_FOUND_IN_THIS_BLACK_LIST = "Customer not found in this black list !";
        public static readonly string TIMEGROUP_NAME_HAS_ALREADY_EXISTED = "Timegroup name has already existed.";
        public static readonly string THIS_RECORD_CANNOT_BE_UPDATED = "This record cannot be updated.";
        public static readonly string THIS_USER_IS_IN_MAPPING = "This user is in mapping.";
        public static readonly string USER_ASSIGNED_IS_INVALID = "User assigned is invalid.";
        public static readonly string USER_ASSIGNED_NOT_FOUND = "User assigned not found.";
        public static readonly string YOU_DONT_HAVE_PERMISSION_TO_DO_THIS = "You don't have permission to do this.";
        public static readonly string CALLFLOW_NAME_HAS_ALREADY_EXISTED = "Callflow name has already existed.";
        #endregion

        #region callflow destination type
        public static readonly string DESTINATION_IVR = "ivr";
        public static readonly string DESTINATION_EXTENSION = "extension";
        public static readonly string DESTINATION_TIMECONDITION = "timecondition";
        public static readonly string DESTINATION_QUEUE = "queue";
        public static readonly string DESTINATION_ANNOUNCEMENT = "announcement";
        public static readonly string DESTINATION_START = "start";
        public static readonly string DESTINATION_TERMINATE = "terminate";
        public static readonly string DESTINATION_CHECK_VIP = "check_vip";
        public static readonly string DESTINATION_CHECK_BLACK_LIST = "check_black_list";
        public static readonly string DESTINATION_ALIAS_IN = "alias_in";
        public static readonly string DESTINATION_ALIAS_OUT = "alias_out";
        public static readonly string DESTINATION_MOBILE_NUMBER = "mobile_number";
        #endregion

        #region callflow next action type
        public static readonly string ACTION_TYPE_TIMECONDITION_MATCHES = "tcd_matches";
        public static readonly string ACTION_TYPE_TIMECONDITION_NON_MATCHES = "tcd_non_matches";
        public static readonly string ACTION_TYPE_IVR_DIGITS = "ivr_digits";
        public static readonly string ACTION_TYPE_IVR_INVALID_DEST = "ivr_invalid_dest";
        public static readonly string ACTION_TYPE_IVR_TIMEOUT_DEST = "ivr_timeout_dest";
        public static readonly string ACTION_TYPE_ANNOUNCEMENT_AFTER_PLAYBACK = "ann_after_playback";
        public static readonly string ACTION_TYPE_QUEUE_FAIL_OVER = "queue_fail_over";
        public static readonly string ACTION_TYPE_START = "start";
        public static readonly string ACTION_TYPE_CHECK_VIP_IS_NORMAL = "check_vip_is_normal";
        public static readonly string ACTION_TYPE_CHECK_VIP_IS_VIP = "check_vip_is_vip";
        public static readonly string ACTION_TYPE_CHECK_BLACK_LIST_IS_BLACK = "check_black_lst_is_black";
        public static readonly string ACTION_TYPE_CHECK_BLACK_LIST_IS_NORMAL = "check_black_lst_is_normal";
        public static readonly string ACTION_TYPE_ALIAS_IN_TO = "alias_in_to";
        public static readonly string ACTION_TYPE_MOBILE_CALL_FAIL = "mobile_call_fail";
        #endregion

        #region hashtype
        public static readonly string HASH_SHA512 = "SHA512";
        public static readonly string HASH_SHA384 = "SHA384";
        public static readonly string HASH_SHA256 = "SHA256";
        #endregion

        #region time group type
        public static readonly string DATE_LIST = "date_list";
        public static readonly string TIME_OF_DAY = "time_of_day";
        #endregion

        #region skill type
        public static readonly string BOOLEAN = "boolean";
        public static readonly string PROFICIENCY = "proficiency";
        #endregion

        #region queue agent_order
        public static readonly string LONGEST_AVAILABLE_AGENT = "longest_available_agent";
        public static readonly string MOST_SCORE_AGENT = "most_score_agent";
        public static readonly string LEAST_SCORE_AGENT = "least_score_agent";
        public static readonly string RANDOM_AGENT = "random_agent";
        public static readonly string ROUND_ROBIN = "round_robin";
        #endregion

        #region queue step condition operators value
        public static readonly string OPERATORS_BOOLEAN_YES = "yes";
        public static readonly string OPERATORS_BOOLEAN_NO = "no";
        #endregion

        #region outbound route type
        public static readonly string OUTBOUND_ROUTE_TYPE_COMMON = "common";
        public static readonly string OUTBOUND_ROUTE_TYPE_PER_AGENT = "per_agent";
        #endregion

        #region author enum
        public enum AUTHOR { TOKEN, SECRET_KEY, TOKEN_OR_KEY };
        #endregion

        #region parallel setting
        public static readonly string CONF_MAX_DEGREE_PARALLELISM = "MAX_DEGREE_PARALLELISM";
        #endregion

        #region agent state
        public static readonly string AGENT_STATE_READY = "ready";
        public static readonly string AGENT_STATE_NOT_READY = "notready";
        public static readonly string AGENT_STATE_CALLING = "calling";
        public static readonly string AGENT_STATE_RINGING = "ringing";
        public static readonly string AGENT_STATE_TALKING = "talking";
        public static readonly string AGENT_STATE_WRAPUP = "wrapup";
        #endregion

        #region outbound number type
        public static string OUTBOUND_HOTLINE = "1[89]00XXXX";
        public static string OUTBOUND_HOTLINE_10 = "1[89]00XXXXXX";
        public static string OUTBOUND_MOBILE = "0[3456789]XXXXXXXX";
        public static string OUTBOUND_MOBILE_INTERNATIONAL = "[3456789]XXXXXXXX";
        public static string OUTBOUND_HOMEPHONE = "02XXXXXXXXX";
        #endregion

        #region common type common setting
        public static string COMMON_TYPE_STRING = "string";
        public static string COMMON_TYPE_NUMBER = "number";
        public static string COMMON_TYPE_DATA = "date";
        public static string COMMON_TYPE_DATETIME = "datetime";
        public static string COMMON_TYPE_BOOLEAN = "boolean";
        public static string COMMON_TYPE_COLOR = "color";
        public static string COMMON_TYPE_PASSWORD = "password";
        #endregion

        #region setting for common setting
        public static string SETTING_FOR_SIZE_FILE = "size_file";
        public static string SETTING_FOR_COMMON = "common";
        public static string SETTING_FOR_EMAIL_SERVICE = "email_service";
        public static string SETTING_FOR_AUTO_SERVICE = "auto_service";
        #endregion

        #region setting key default common setting
        public static string COMMON_SETTING_KEY_SIZE_FILE = "max_size_file_system";
        #endregion

        #region list colors to generate default avatar
        public static List<string> COLOR_CODES = new List<string> {
            "#EEAD0E", "#8bbf61", "#DC143C", "#CD6889", "#8B8386", "#800080", "#9932CC", "#009ACD", "#00CED1", "#03A89E",
            "#00C78C", "#00CD66", "#66CD00", "#EEB422", "#FF8C00", "#EE4000", "#388E8E", "#8E8E38", "#7171C6" };
        #endregion  

        #region extension file
        public static readonly string EXTENSION_FILE_AVATAR = ".jpg";
        public static readonly string EXTENSION_FILE_RECORDING = ".wav";
        #endregion

        #region workscreen type
        public static readonly string WORKSCREEN_TYPE_POPUP = "screen_popup";
        public static readonly string WORKSCREEN_TYPE_APPLICATION = "screen_application";
        #endregion

        #region object service name
        public static readonly string AUTHENTICATION_SERVICE = "Authentication";
        public static readonly string COMMON_SETTING_SERVICE = "Common Setting";
        public static readonly string VIP_LIST_SERVICE = "Vip List";
        public static readonly string HOTLINE_SERVICE = "Hotline";
        public static readonly string INTEGRATION_APPLICATION_SERVICE = "Integration";
        public static readonly string MODULE_SERVICE = "Module";
        public static readonly string OUTBOUND_SERVICE = "Outbound";
        public static readonly string PERMISSION_SERVICE = "Permission";
        public static readonly string PERMISSION_OBJECT_SERVICE = "Permission Object";
        public static readonly string PHONE_NUMBER_DO_NOT_CALL_SERVICE = "Do Not Call";
        public static readonly string PROFILE_SERVICE = "Profile";
        public static readonly string ROLE_HIERARCHY_SERVICE = "Role Hierarchy";
        public static readonly string SKILL_SERVICE = "Skill";
        public static readonly string STATE_LIST_SERVICE = "State List";
        public static readonly string SYSTEM_RECORDING_SERVICE = "System Recording";
        public static readonly string TIMEGROUP_SERVICE = "Working Hour";
        public static readonly string WEBHOOK_SETTING_SERVICE = "Webhook Setting";
        public static readonly string WORK_SCREEN_SERVICE = "Work Screen";
        public static readonly string AGENT_GROUP_SERVICE = "Agent Group";
        public static readonly string AGENT_STATE_SERVICE = "Agent State";
        public static readonly string CHANGE_PASSWORD_LOG_SERVICE = "Change Password Log";
        public static readonly string MAP_AGENT_SKILL_SERVICE = "Map Agent Skill";
        public static readonly string QUEUE_SERVICE = "Queue";
        public static readonly string TENANTS_SERVICE = "Tenants";
        public static readonly string CALLFLOW_SERVICE = "Call Flow";
        public static readonly string CALLFLOW_COMPONENT_SERVICE = "Call Flow Component";
        public static readonly string COMPONENT_ACTION_SERVICE = "Component Action Service";
        public static readonly string USER_MANAGEMENT_SERVICE = "User Management";
        public static readonly string CONTACT_BLACK_LIST_CONFIG_SERVICE = "Contact Black List Config";
        public static readonly string CONTACT_BLACK_LIST_SERVICE = "Contact Black List";
        public static readonly string DEFAULT_COMMON_SETTING_SERVICE = "Default Common Setting";
        public static readonly string DEFAULT_HOTLINE_LIBRARY_SERVICE = "Default Hotline Library";
        public static readonly string DEFAULT_MODULE_SERVICE = "Default Module";
        public static readonly string DEFAULT_PERMISSION_OBJECT_SERVICE = "Default Permission Object";
        public static readonly string DEFAULT_STATE_LIST_SERVICE = "Default State List";
        public static readonly string DEFAULT_SYSTEM_RECORDING_SERVICE = "Default System Recording";
        public static readonly string DEFAULT_WEBHOOK_EVENT_SERVICE = "Default Webhook Event";
        public static readonly string DEFAULT_ASTERISK_TRUNKING_SERVICE = "Default Asterisk Trunking";
        public static readonly string DEFAULT_CALLFLOW_COMPONENT_SERVICE = "Default Callflow Componenent";
        public static readonly string DEFAULT_CALLFLOW_SERVICE = "Default Callflow";
        public static readonly string DEFAULT_CALLFLOW_NEXT_ACTION_SERVICE = "Default Callflow Next Action";
        public static readonly string DEFAULT_ASTERISK_CONNECT_SERVICE = "Default Asterisk Connect";
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
        public const string COMPLETED_QUICK_SETTING_SUCCESSFULLY = "Completed quick setting successfully.";
        public const string COMPLETED_QUICK_SETTING_FAILED = "Completed quick setting failed.";
        public const string UPDATED_IS_ACTIVE_SUCCESSFULLY = "Updated is active successfully.";
        public const string UPDATED_IS_ACTIVE_FAILED = "Updated is active failed.";
        public const string CHANGE_PASSWORD_SUCCESSFULLY = "Change password successfully.";
        public const string CHANGE_PASSWORD_FAILED = "Change password failed.";
        public const string IMPORT_DATA_TO_VIP_LIST_SUCCESSFULLY = "Import data to the VIP list successfully.";
        public const string IMPORT_DATA_TO_VIP_LIST_FAILED = "Import data to the VIP list failed.";
        public const string ADD_CUSTOMER_TO_VIP_LIST_SUCCESSFULLY = "Add customers to the VIP list successfully.";
        public const string ADD_CUSTOMER_TO_VIP_LIST_FAILED = "Add customers to the VIP list failed.";
        public const string UPDATE_CUSTOMER_ON_VIP_LIST_SUCCESSFULLY = "Updated customers on the VIP list successfully.";
        public const string UPDATE_CUSTOMER_ON_VIP_LIST_FAILED = "Updated customers on the VIP list failed.";
        public const string REMOVE_CUSTOMER_FROM_VIP_LIST_SUCCESSFULLY = "Remove customers from the VIP list successfully.";
        public const string REMOVE_CUSTOMER_FROM_VIP_LIST_FAILED = "Remove customers from the VIP list failed.";
        public const string ADD_USER_TO_PROFILE_SUCCESSFULLY = "Add user to profile successfully.";
        public const string ADD_USER_TO_PROFILE_FAILED = "Add user to profile failed.";
        public const string UPDATED_PERMISSION_ON_PROFILE_SUCCESSFULLY = "Updated permissions on the profile successfully.";
        public const string UPDATED_PERMISSION_ON_PROFILE_FAILED = "Updated permissions on the profile failed.";
        public const string REMOVE_USER_FROM_PROFILE_SUCCESSFULLY = "Remove users from the profile successfully.";
        public const string REMOVE_USER_FROM_PROFILE_FAILED = "Remove users from the profile failed.";
        public const string DELETED_AND_TRANSFERED_ROLE_SUCCESSFULLY = "Deleted and transferred roles successfully.";
        public const string DELETED_AND_TRANSFERED_ROLE_FAILED = "Deleted and transferred roles failed.";
        public const string ADD_CUSTOMER_TO_BLACK_LIST_SUCCESSFULLY = "Add customers to the Black list successfully.";
        public const string REMOVE_CUSTOMER_FROM_BLACK_LIST_SUCCESSFULLY = "Remove customers from the Black list successfully.";
        public const string IMPORT_DATA_FAILED = "Import data failed.";
        public const string CLONED_CALLFLOW_SUCCESSFULLY = "Cloned callflow successfully.";
        public const string CLONED_CALLFLOW_FAILED = "Cloned callflow failed.";
        public const string APPLY_CALLFLOW_SUCCESSFULLY = "Apply callflow successfully.";
        public const string APPLY_CALLFLOW_FAILED = "Apply callflow failed.";
        public const string UPDATED_COMPONENT_SUCCESSFULLY = "Updated component failed.";
        public const string UPDATED_COMPONENT_FAILED = "Updated component failed.";
        public const string CREATED_COMPONENT_SUCCESSFULLY = "Created component successfully.";
        public const string CREATED_COMPONENT_FAILED = "Created component failed.";
        public const string DELETED_COMPONENT_SUCCESSFULLY = "Deleted component successfully.";
        public const string DELETED_COMPONENT_FAILED = "Deleted component failed.";
        public const string CREATED_COMPONENT_ACTION_SUCCESSFULLY = "Created component action successfully.";
        public const string CREATED_COMPONENT_ACTION_FAILED = "Created component action failed.";
        public const string DELETED_COMPONENT_ACTION_SUCCESSFULLY = "Deleted component action successfully.";
        public const string DELETED_COMPONENT_ACTION_FAILED = "Deleted component action failed.";
        public const string UPDATED_COMPONENT_ACTION_SUCCESSFULLY = "Updated component action successfully.";
        public const string UPDATED_COMPONENT_ACTION_FAILED = "Updated component action failed.";
        public const string UPLOADED_FILE_SUCCESSFULLY = "Uploaded file successfully.";
        public const string UPLOADED_FILE_FAILED = "Uploaded file failed.";
        public const string DELETED_FILE_SUCCESSFULLY = "Deleted file successfully.";
        public const string DELETED_FILE_FAILED = "Deleted file failed.";
        public const string CREATE_ALL_COMMON_SETTING_SUCCESSFULLY = "Created all common setting successfully.";
        public const string CREATE_ALL_COMMON_SETTING_FAILED = "Created all common setting failed.";
        public const string IMPORT_DATA_TO_BLACK_LIST_SUCCESSFULLY = "Import data to the Black list successfully.";
        public const string IMPORT_DATA_TO_BLACK_LIST_FAILED = "Import data to the Black list failed.";
        public const string ADD_PHONE_DO_NOT_CALL_SUCCESSFULLY = "Add phone number do not call successfully.";
        public const string ADD_PHONE_DO_NOT_CAL_FAILED = "Add phone number do not call failed.";
        public const string REMOVE_PHONE_DO_NOT_CALL_SUCCESSFULLY = "Remove phone number do not call successfully";
        public const string REMOVE_PHONE_DO_NOT_CALL_FAILED = "Remove phone number do not call failed.";
        public const string IMPORT_DATA_PHONE_DO_NOT_CALL_SUCCESSFULLY = "Import data phone number do not call successfully.";
        public const string IMPORT_DATA_PHONE_DO_NOT_CALL_FAILED = "Import data phone number do not call failed.";
        public const string ADD_INTEGRATION_CONFIG_SUCCESSFULLY = "Add Integration config successfully.";
        public const string ADD_INTEGRATION_CONFIG_FAILED = "Add Integration config failed.";
        public const string MAPPING_USER_INTEGRATION_SUCCESSFULLY = "User integration mapped successfully.";
        public const string MAPPING_USER_INTEGRATION_FAILED = "User integration mapped failed.";
        public const string MAPPING_OBJECT_FIELD_INTEGRATION_SUCCESSFULLY = "Object and Field integration mapped successfully.";
        public const string MAPPING_OBJECT_FIELD_INTEGRATION_FAILED = "Object and Field integration mapped failed.";
        public const string ADD_CUSTOMER_TO_BLACK_LIST_FAILED = "Add customers to the Black list failed.";
        public const string ADD_BLACK_LIST_CONFIG_SUCCESSFULLY = "Add black list config successfully.";
        public const string ADD_DEFAULT_HOTLINE_LIBRARY_SUCCESSFULLY = "Add default hotline library successfully.";
        public const string ADD_DEFAULT_HOTLINE_LIBRARY_FAILED = "Add default hotline library failed.";
        public const string REMOVE_DEFAULT_HOTLINE_LIBRARY_SUCCESSFULLY = "Remove default hotline library successfully";
        public const string REMOVE_DEFAULT_HOTLINE_LIBRARY_FAILED = "Remove default hotline library failed.";
        public const string IMPORT_DATA_DEFAULT_HOTLINE_LIBRARY_SUCCESSFULLY = "Import data default hotline library successfully.";
        public const string IMPORT_DATA_DEFAULT_HOTLINE_LIBRARY_FAILED = "Import data default hotline library failed.";

        #endregion

        #region default role
        public static readonly string DEFAULT_ROLE_DIRECTORS = "Directors";
        public static readonly string DEFAULT_ROLE_SUPERVISOR = "Supervisor";
        public static readonly string DEFAULT_ROLE_AGENT = "Agent";
        public static readonly string DEFAULT_ROLE_ADMIN = "Admin";
        #endregion

        #region default profile
        public static readonly string DEFAULT_PROFILE_DIRECTORS = "Directors";
        public static readonly string DEFAULT_PROFILE_SUPERVISOR = "Supervisor";
        public static readonly string DEFAULT_PROFILE_AGENT = "Agent";
        public static readonly string DEFAULT_PROFILE_ADMIN = "Admin";
        #endregion

        #region object name
        public static readonly string OBJECT_AUTHENTICATION_SERVICE = "Authentication Service";
        #endregion

        #region service name
        #endregion

        #region hashtag redis
        public static readonly string HASHTAG_CONNECTION = "connection";
        #endregion

        #region default root role
        public static readonly Guid ROOT_ROLE = Guid.Parse("7F126686-0853-4EA3-B9CD-4E8F7D34A77C");
        #endregion

        #region customer type
        public static readonly string CUSTOMER_TYPE_TRIAL = "trial";
        public static readonly string CUSTOMER_TYPE_BASIC = "basic";
        public static readonly string CUSTOMER_TYPE_STANDARD = "standard";
        public static readonly string CUSTOMER_TYPE_ADVANCED = "advanced";
        #endregion

        #region common setting key 
        public static readonly string COMMON_SETTING_KEY_USE_WEBHOOK = "use_webhook";
        #endregion

        #region excel template 
        public static readonly string CONTACT_PRIORITY_LIST_TEMPLATE = "File/vip_list_template.xlsx";
        public static readonly string HOTLINE_LIBRARY_TEMPLATE = "File/hotline_library_template.xlsx";
        public static readonly string PHONE_NUMBER_DO_NOT_CALL_TEMPLATE = "File/do_not_call_template.xlsx";
        public static readonly string CONTACT_BLACK_LIST_TEMPLATE = "File/black_list_template.xlsx";
        #endregion
    }
}
