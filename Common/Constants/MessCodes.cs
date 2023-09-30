﻿namespace Common
{
    public class MessCodes
    {
        public static readonly int ACCOUNT_LOCKED = 1;
        public static readonly int USERNAME_OR_PASSWORD_INCORECT = 2;
        public static readonly int FORGOT_PASSWORD_FAIL = 3;
        public static readonly int KEY_INVALID = 4;
        public static readonly int UPDATE_FAIL = 5;
        public static readonly int LOGOUT_FAIL = 6;
        public static readonly int DELETE_FAIL = 7;
        public static readonly int EMAIL_IS_EXISTS = 8;
        public static readonly int PERMISSION_OBJECT_IS_EXISTS = 9;
        public static readonly int MODULE_IS_EXISTS = 10;
        public static readonly int USERNAME_IS_EXISTS = 11;
        public static readonly int EMAIL_DOES_NOT_EXISTS = 12;
        public static readonly int PUBLIC_KEY_HAS_EXPIRED = 13;
        public static readonly int YOU_HAVE_SUMITTED_REQUEST = 14;
        public static readonly int CREATE_BUSINESS_TYPE_FAIL = 15;
        public static readonly int OLD_PASSWORD_IS_NOT_CORRECT = 16;
        public static readonly int MAX_WRONG_PASSWORD_NUMBER = 17;
        public static readonly int THE_REMAINING_LOGIN = 18;
        public static readonly int DUPLICATE_PASSWORD = 19;
        public static readonly int PASSWORD_DOES_NOT_MATCH = 20;
        public static readonly int EMPLOYEE_CREATION_LIMIT = 21;
        public static readonly int BUILD_TENANT_FAIL = 22;
        public static readonly int USERNAME_DOES_NOT_EXISTS = 23;
        public static readonly int ERROR_DELETE_USER_IN_SSO = 24;
        public static readonly int ASTERISK_ERROR = 25;
        public static readonly int USER_NOT_FOUND = 26;
        public static readonly int HOTLINE_NUMBER_ALREADY_EXISTS = 27;
        public static readonly int HOTLINE_NOT_FOUND = 28;
        public static readonly int PLEASE_CHOOSE_MP3_FILE = 29;
        public static readonly int PLEASE_CHOOSE_MP3_OR_WAV_FILE = 30;
        public static readonly int NAME_IS_ALREADY_EXISTS = 31;
        public static readonly int DATA_NOT_FOUND = 32;
        public static readonly int CALLFLOW_NOT_FOUND = 33;
        public static readonly int HOTLINE_IS_USED_IN_ANOTHER_CALLFLOW = 34;
        public static readonly int CANT_CONNECT_TO_ASTERISK = 35;
        public static readonly int COMPONENT_IS_NOT_COMPLETED_YET = 36;
        public static readonly int COMPONENT_NOT_FOUND = 37;
        public static readonly int DUPLICATE_ACTION = 38;
        public static readonly int SYSTEM_RECORDING_IS_IN_USE = 39;
        public static readonly int TIME_GROUP_IS_IN_USE = 40;
        public static readonly int HOTLINE_CANNOT_DELETE = 41;
        public static readonly int AGENTGROUP_NAME_IS_ALREADY_EXISTS = 42;
        public static readonly int AGENTGROUP_NOT_FOUND = 43;
        public static readonly int USER_NOT_FOUND_IN_GROUP = 44;
        public static readonly int SYSTEM_RECORDING_CANNOT_DELETE = 45;
        public static readonly int FILE_SOUND_NOT_FOUND = 46;
        public static readonly int SKILL_NAME_IS_ALREADY_EXISTS = 47;
        public static readonly int SKILL_NOT_FOUND = 48;
        public static readonly int SKILL_OR_USER_NOT_FOUND = 49;
        public static readonly int AGENT_GROUP_OR_USER_NOT_FOUND = 50;
        public static readonly int MODULE_NAME_IS_ALREADY_EXISTS = 51;
        public static readonly int ASTERISK_SERVER_IS_FULL = 52;
        public static readonly int ASTERISK_SERVER_NOT_FOUND = 53;
        public static readonly int EXTENSION_NUMBER_RANGE_NOT_FOUND = 54;
        public static readonly int EXTENSION_NUMBER_IS_MAX = 55;
        public static readonly int OBJECT_NAME_HAS_ALREADY_EXISTED = 56;
        public static readonly int AGENT_IS_ALREADY_IN_GROUP = 57;
        public static readonly int AGENT_NOT_EXISTS_OR_IS_ALREADY_IN_SKILL = 58;
        public static readonly int ONLY_CHOOSE_GROUP_OR_STEP = 59;
        public static readonly int CANT_CHOOSE_AGENT_ORDER = 60;
        public static readonly int COMPONENT_INVALID = 61;
        public static readonly int DATA_INVALID = 62;
        public static readonly int START_COMPONENT_IS_NOT_COMPLETED_YET = 63;
        public static readonly int TCD_COMPONENT_MATCHES_IS_NOT_COMPLETED_YET = 64;
        public static readonly int TCD_COMPONENT_NON_MATCHES_IS_NOT_COMPLETED_YET = 65;
        public static readonly int IVR_COMPONENT_TIMEOUT_IS_NOT_COMPLETED_YET = 66;
        public static readonly int IVR_COMPONENT_INVALID_IS_NOT_COMPLETED_YET = 67;
        public static readonly int IVR_COMPONENT_RECORDING_IS_NOT_COMPLETED_YET = 68;
        public static readonly int IVR_COMPONENT_DIGIT_IS_NOT_COMPLETED_YET = 69;
        public static readonly int ANNOUNCEMENT_COMPONENT_RECORDING_IS_NOT_COMPLETED_YET = 70;
        public static readonly int ANNOUNCEMENT_COMPONENT_AFTER_PLAYBACK_IS_NOT_COMPLETED_YET = 71;
        public static readonly int QUEUE_COMPONENT_QUEUE_NOT_FOUND = 72;
        public static readonly int QUEUE_COMPONENT_RECORDING_NOT_FOUND = 73;
        public static readonly int QUEUE_COMPONENT_QUEUE_FAIL_OVER_IS_NOT_COMPLETED_YET = 74;
        public static readonly int EXTENSION_COMPONENT_IS_NOT_COMPLETED_YET = 75;
        public static readonly int EXTENSION_COMPONENT_EXTENSION_NOT_FOUND = 76;
        public static readonly int VALIDATE_COMPONENT_FAILED = 77;
        public static readonly int CREATE_TENANT_FAILED = 78;
        public static readonly int SETTING_KEY_ALREADY_EXISTS = 79;
        public static readonly int CONNECT_TO_VOICE_PORTAL_SEVER_FAILED = 80;
        public static readonly int CONNECT_TO_PBX_SEVER_FAILED = 81;
        public static readonly int STATE_KEY_ALREADY_EXISTS = 82;
        public static readonly int TENANT_NOT_FOUND = 83;
        public static readonly int WEBHOOK_EVENT_KEY_IS_ALREADY_EXISTS = 84;
        public static readonly int WEBHOOK_HEADER_KEY_IS_ALREADY_EXISTS = 85;
        public static readonly int WEBHOOK_SETTING_NOT_FOUND = 86;
        public static readonly int WEBHOOK_EVENT_NAME_IS_ALREADY_EXISTS = 87;
        public static readonly int NEW_PASSWORD_CANNOT_SAME_OLD_PASSWORD = 88;
        public static readonly int DUPLICATE_RECORDING_ID = 89;
        public static readonly int DUPLICATE_SKILL = 90;
        public static readonly int ADMIN_PROFILE_CANNOT_BE_DELETED = 91;
        public static readonly int PROFILE_NOT_FOUND = 92;
        public static readonly int WEIGHT_VALUE_CANNOT_BE_LESS_THAN_0 = 93;
        public static readonly int START_COMPONENT_NOT_FOUND = 94;
        public static readonly int ROLE_NOT_FOUND = 95;
        public static readonly int STATE_NAME_IS_ALREADY_EXISTS = 96;
        public static readonly int DUPLICATE_POSITION = 97;
        public static readonly int AGENT_NOT_FOUND = 98;
        public static readonly int OUTBOUND_ROUTE_NOT_FOUND = 99;
        public static readonly int THIS_ROLE_CANNOT_BE_DELETED = 100;
        public static readonly int THIS_ROLE_CANNOT_BE_TRANSFERED = 101;
        public static readonly int TENANT_IS_DEACTIVE = 102;
        public static readonly int EXPIRED_TRIAL_PERIOD = 103;
        public static readonly int CLONE_CALLFLOW_FAILED = 104;
        public static readonly int PLEASE_CHOOSE_EXCEL_FILE = 105;
        public static readonly int GROUP_NAME_IS_ALREADY_EXISTS = 106;
        public static readonly int CONFIG_NOT_FOUND = 107;
        public static readonly int CONTACT_CONFIG_IS_IN_USE = 108;
        public static readonly int REPORT_TO_NOT_FOUND = 109;
        public static readonly int CANNOT_SELECT_ROLE_DIRECTOR = 110;
        public static readonly int USERNAME_OR_EXTENSION_NUMBER_IS_EXISTS = 111;
        public static readonly int INVALID_REPORT_TO = 112;
        public static readonly int CUSTOMER_NOT_FOUND_IN_THIS_LIST = 113;
        public static readonly int CANT_CONNECT_TO_THIRD_PARTY = 114;
        public static readonly int CHECK_VIP_COMPONENT_IS_NORMAL_IS_NOT_COMPLETED_YET = 115;
        public static readonly int CHECK_VIP_COMPONENT_IS_VIP_IS_NOT_COMPLETED_YET = 116;
        public static readonly int THIS_USER_CANNOT_BE_DELETED = 117;
        public static readonly int USER_REPORT_TO_IS_INVALID = 118;
        public static readonly int CANT_SYNC_DATA_THIRD_PART = 119;
        public static readonly int INTEGRATION_IS_ALREADY_EXISTS = 120;        
        public static readonly int ONLY_CHOOSE_ONE_START_COMPONENT = 121;        
        public static readonly int ALIAS_IN_IS_NOT_COMPLETED_YET = 122;        
        public static readonly int MODULE_NOT_FOUND = 123;        
        public static readonly int ADMIN_PROFILE_CANNOT_BE_UPDATED = 124;        
        public static readonly int ROLE_NAME_HAS_ALREADY_EXISTED = 125;        
        public static readonly int ALIAS_OUT_ID_IS_IN_USE = 126;        
        public static readonly int TRIAL_CUSTOMER_CANNOT_CREATE_MULTI_HOTLINE = 127;        
        public static readonly int SYSTEM_RECORDING_NOT_FOUND = 128;        
        public static readonly int QUEUE_NOT_FOUND = 129;        
        public static readonly int TIME_GROUP_NOT_FOUND = 130;        
        public static readonly int ALIAS_OUT_NOT_FOUND = 131;    
        public static readonly int ALIAS_OUT_IS_NEVER_USED = 132;    
        public static readonly int ONLY_ROOT_USER_CAN_DO_THIS = 133;    
        public static readonly int PERMISSION_OBJECT_HAS_ALREADY_EXISTED = 134;    
        public static readonly int PROFILE_HAS_ALREADY_EXISTED = 135;    
        public static readonly int PERMISSION_OBJECT_NOT_FOUND = 136;    
        public static readonly int ADMINISTRATOR_CANNOT_BE_REMOVED_ADMIN_PROFILE = 137;
        public static readonly int PHONRE_NUMBER_DO_NOT_CALL_IS_ALREADY_EXISTS = 138;
        public static readonly int MOBILE_NUMBER_COMPONENT_IS_NOT_COMPLETED_YET = 139;
        public static readonly int NODE_MOBILE_CALL_FAIL_IS_NOT_COMPLETED_YET = 140;
        public static readonly int AUTHEN_FAILED = 141;
        public static readonly int COMMON_SETTING_NOT_FOUND = 142;
        public static readonly int CHECK_BLACK_LIST_COMPONENT_IS_NORMAL_IS_NOT_COMPLETED_YET = 143;
        public static readonly int CHECK_BLACK_LIST_COMPONENT_IS_BLACK_LIST_IS_NOT_COMPLETED_YET = 144;
        public static readonly int OUTBOUND_NUMBER_HAS_ALREADY_EXISTED = 145;
        public static readonly int EXTENSION_RANGE_NOT_FOUND = 146;
        public static readonly int TRUNK_NOT_FOUND = 147;
        public static readonly int SKILL_IS_IN_USE = 148;
        public static readonly int QUEUE_IS_IN_USE = 149;
        public static readonly int TIMEGROUP_NAME_HAS_ALREADY_EXISTED = 150;
        public static readonly int SKILL_NAME_HAS_ALREADY_EXISTED = 151;
        public static readonly int AGENTGROUP_NAME_HAS_ALREADY_EXISTED = 152;
        public static readonly int QUEUE_NAME_HAS_ALREADY_EXISTED = 153;
        public static readonly int THIS_RECORD_CANNOT_BE_UPDATED = 154;
        public static readonly int USER_ASSIGNED_IS_INVALID = 155;
        public static readonly int ASSIGN_DATA_FAILED = 156;
        public static readonly int THIS_USER_IS_IN_MAPPING = 157;
        public static readonly int USER_ASSIGNED_NOT_FOUND = 158;
        public static readonly int YOU_DONT_HAVE_PERMISSION_TO_DO_THIS = 159;
        public static readonly int CALLFLOW_NAME_HAS_ALREADY_EXISTED = 160;
        public static readonly int WORK_SCREEN_NAME_HAS_ALREADY_EXISTED = 161;
        public static readonly int BANK_NAME_IS_ALREADY_EXISTS = 162;
        public static readonly int BANK_NOT_FOUND = 163;
        public static readonly int GROUP_NOT_FOUND = 164;
    }
}
