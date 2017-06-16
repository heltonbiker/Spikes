using System;

namespace StCamSWareCS.SettingCtrl
{
	public interface ISettingCtrl
	{
		SettingIDs SettingID{get;set;}
	}
	public enum SettingIDs
	{
		UNKNOWN,
		DISPLAY_MODE,
		ASPECT_MODE,
		PW_OFFSETX,
		PW_OFFSETY,
		PW_WIDTH,
		PW_HEIGHT,
		PM_OFFSETX,
		PM_OFFSETY,
		PM_WIDTH,
		PM_HEIGHT,
		PD_OFFSETX,
		PD_OFFSETY,
		PD_WIDTH,
		PD_HEIGHT,
		ALC_MODE,
		GAIN,
		SHUTTER,
		ALC_TARGET,
		ALC_TOLERANCE,
		ALC_THRESHOLD,
		AGC_RANGE,
		AGC_RANGE_MIN,
		AGC_RANGE_MAX,
		AEE_RANGE,
		AEE_RANGE_MIN,
		AEE_RANGE_MAX,
		DIGITAL_GAIN,
		WB_MODE,
		WB_GAIN_R,
		WB_GAIN_GR,
		WB_GAIN_GB,
		WB_GAIN_B,
		WB_TARGET_R,
		WB_TARGET_B,
		GAMMA_Y_MODE,
		GAMMA_Y_VALUE,
		CAMERA_GAMMA_VALUE,
		DIGITAL_CLAMP,
		SHARPNESS_MODE,
		SHARPNESS_GAIN,
		SHARPNESS_CORING,
		HUE_SATURATION_MODE,
		SATURATION,
		HUE,
		GAMMA_R_MODE,
		GAMMA_R_VALUE,
		GAMMA_GR_MODE,
		GAMMA_GR_VALUE,
		GAMMA_GB_MODE,
		GAMMA_GB_VALUE,
		GAMMA_B_MODE,
		GAMMA_B_VALUE,
		SCAN_MODE,
		SCAN_OFFSET_X,
		SCAN_OFFSET_Y,
		SCAN_WIDTH,
		SCAN_HEIGHT,
		H_BIN_SKIP,
		V_BIN_SKIP,
		H_BIN_SUM,
		V_BIN_SUM,
		CLOCK_MODE,
		VBLANKING_FOR_FPS,
		PIXEL_FORMAT,
		COLOR_INTERPOLATION,
		MIRROR_MODE,
		ROTATION_MODE,
		AGC_CHECK,
		AEE_CHECK,
		SHUTTER_BTN,
		AWB_CHECK,
		HUESATURATION_CHECK,
		GAMMA_CHECK,
		SHARPNESS_CHECK,
		CAMERA_SEETING_BTN,

		//ALC Weight
		ALC_WEIGHT_00,
		ALC_WEIGHT_01,
		ALC_WEIGHT_02,
		ALC_WEIGHT_03,
		ALC_WEIGHT_04,
		ALC_WEIGHT_05,
		ALC_WEIGHT_06,
		ALC_WEIGHT_07,
		ALC_WEIGHT_08,
		ALC_WEIGHT_09,
		ALC_WEIGHT_10,
		ALC_WEIGHT_11,
		ALC_WEIGHT_12,
		ALC_WEIGHT_13,
		ALC_WEIGHT_14,
		ALC_WEIGHT_15,

		CONTROL_AREA_X_0,
		CONTROL_AREA_X_1,
		CONTROL_AREA_X_2,

		CONTROL_AREA_Y_0,
		CONTROL_AREA_Y_1,
		CONTROL_AREA_Y_2,

		//Color Matrix
		COLOR_MATRIX_00,
		COLOR_MATRIX_01,
		COLOR_MATRIX_02,
		COLOR_MATRIX_03,
		COLOR_MATRIX_10,
		COLOR_MATRIX_11,
		COLOR_MATRIX_12,
		COLOR_MATRIX_13,
		COLOR_MATRIX_20,
		COLOR_MATRIX_21,
		COLOR_MATRIX_22,
		COLOR_MATRIX_23,

		AVI_FILE_FORMAT,
		AVI_COMPRESSOR,
		AVI_QUALITY,
		AVI_LENGTH,

		OUTPUT_FPS,

		//Defect Pixel
		DEFECT_PIXEL_CORRECTION_MODE,
		DEFECT_PIXEL_POS_X_00,
		DEFECT_PIXEL_POS_Y_00,
		DEFECT_PIXEL_POS_X_LAST = DEFECT_PIXEL_POS_X_00 + 63 * 2,
		DEFECT_PIXEL_POS_Y_LAST = DEFECT_PIXEL_POS_X_LAST + 1,
	}

}