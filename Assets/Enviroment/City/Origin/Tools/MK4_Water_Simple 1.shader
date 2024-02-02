// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "MK4/Simple Water"
{
	Properties
	{
		_WaterColor("Water Color", Color) = (0.4926471,0.8740366,1,1)
		_NormalTiling("Normal Tiling", Range( 0 , 300)) = 50
		_WaveSpeed("Wave Speed", Range( 0 , 5)) = 0
		_FoamColor("Foam Color", Color) = (1,1,1,0)
		_FoamDist("Foam Dist", Range( 0 , 100)) = 0.1
		_Opacity("Opacity", Range( 0 , 1)) = 0.5
		_Gloss("Gloss", Range( 0 , 1)) = 0
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_Wave("Wave", 2D) = "bump" {}
		_Bump1Scale("Bump1 Scale", Range( 0 , 3)) = 1
		_Bump2Scale("Bump2 Scale", Range( 0 , 3)) = 1
		_WaterDepth("Water Depth", Range( 0 , 55)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" }
		Cull Off
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#pragma target 3.0
		#pragma surface surf Standard alpha:fade keepalpha noshadow exclude_path:deferred 
		struct Input
		{
			float2 uv_texcoord;
			float4 screenPos;
		};

		uniform sampler2D _Wave;
		uniform float _Bump1Scale;
		uniform float _NormalTiling;
		uniform float _WaveSpeed;
		uniform float _Bump2Scale;
		uniform float4 _FoamColor;
		uniform sampler2D _CameraDepthTexture;
		uniform float _FoamDist;
		uniform float4 _WaterColor;
		uniform float _Metallic;
		uniform float _Gloss;
		uniform float _WaterDepth;
		uniform float _Opacity;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 temp_cast_0 = (( _NormalTiling * 1.5 )).xx;
			float4 temp_output_88_0 = ( _Time * _WaveSpeed );
			float2 uv_TexCoord96 = i.uv_texcoord * temp_cast_0 + temp_output_88_0.xy;
			float2 temp_cast_2 = (_NormalTiling).xx;
			float temp_output_214_0 = ( _Time.y * _WaveSpeed );
			float2 temp_cast_3 = (temp_output_214_0).xx;
			float2 uv_TexCoord212 = i.uv_texcoord * temp_cast_2 + temp_cast_3;
			float2 temp_cast_4 = (( _NormalTiling + 10.0 )).xx;
			float2 uv_TexCoord218 = i.uv_texcoord * temp_cast_4 + ( 1.0 - temp_output_88_0 ).xy;
			float2 temp_cast_6 = (( _NormalTiling * 0.5 )).xx;
			float2 temp_cast_7 = (( ( 1.0 - temp_output_214_0 ) * 0.6 )).xx;
			float2 uv_TexCoord232 = i.uv_texcoord * temp_cast_6 + temp_cast_7;
			float3 normalizeResult225 = normalize( BlendNormals( BlendNormals( BlendNormals( UnpackScaleNormal( tex2D( _Wave, uv_TexCoord96 ) ,_Bump1Scale ) , UnpackScaleNormal( tex2D( _Wave, uv_TexCoord212 ) ,_Bump1Scale ) ) , UnpackScaleNormal( tex2D( _Wave, uv_TexCoord218 ) ,_Bump1Scale ) ) , UnpackScaleNormal( tex2D( _Wave, uv_TexCoord232 ) ,_Bump2Scale ) ) );
			o.Normal = normalizeResult225;
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float screenDepth164 = LinearEyeDepth(UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture,UNITY_PROJ_COORD(ase_screenPos))));
			float distanceDepth164 = abs( ( screenDepth164 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( _FoamDist ) );
			float4 lerpResult157 = lerp( _FoamColor , float4(0,0,0,0) , distanceDepth164);
			float4 clampResult191 = clamp( lerpResult157 , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			float4 clampResult244 = clamp( ( clampResult191 + _WaterColor ) , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			o.Albedo = clampResult244.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Gloss;
			float screenDepth226 = LinearEyeDepth(UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture,UNITY_PROJ_COORD(ase_screenPos))));
			float distanceDepth226 = abs( ( screenDepth226 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( _WaterDepth ) );
			float lerpResult228 = lerp( 0.0 , 1.0 , distanceDepth226);
			float clampResult245 = clamp( ((-1.0 + (_Opacity - 0.0) * (0.0 - -1.0) / (1.0 - 0.0)) + (lerpResult228 - 0.0) * ((1.0 + (_Opacity - 0.0) * (2.0 - 1.0) / (1.0 - 0.0)) - (-1.0 + (_Opacity - 0.0) * (0.0 - -1.0) / (1.0 - 0.0))) / (1.0 - 0.0)) , 0.0 , 1.0 );
			o.Alpha = clampResult245;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14301
16;466;1454;567;2220.998;-62.81745;2.004515;True;True
Node;AmplifyShaderEditor.CommentaryNode;199;-2827.374,-925.0059;Float;False;914.394;362.5317;Comment;5;89;15;88;224;239;Wave Speed;1,1,1,1;0;0
Node;AmplifyShaderEditor.TimeNode;89;-2701.36,-875.0057;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;15;-2777.374,-677.473;Float;False;Property;_WaveSpeed;Wave Speed;2;0;Create;True;0;0.01;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;216;-2614.801,-431.6432;Float;False;Property;_NormalTiling;Normal Tiling;1;0;Create;True;50;202;0;300;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;88;-2374.429,-739.9845;Float;False;2;2;0;FLOAT4;0.0,0,0,0;False;1;FLOAT;0.0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;214;-2411.219,-609.246;Float;False;2;2;0;FLOAT;0,0,0,0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;239;-2173.165,-824.7407;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;1.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;212;-2009.059,-622.2024;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;55,55;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;221;-2158.917,-507.6915;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;10.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;167;-1665.902,808.174;Float;True;Property;_FoamDist;Foam Dist;4;0;Create;True;0.1;1;0;100;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;224;-2183.696,-902.5143;Float;False;Property;_Bump1Scale;Bump1 Scale;9;0;Create;True;1;0.33;0;3;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;96;-2001.987,-816.4404;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;55,55;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;233;-2333.452,-174.1641;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;220;-2186.724,-396.8839;Float;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TexturePropertyNode;223;-2039.743,-1126.066;Float;True;Property;_Wave;Wave;8;0;Create;True;None;f4bdb1c700d01dd4b858c13998749c9f;True;bump;Auto;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.ColorNode;159;-1656.591,619.5612;Float;False;Constant;_Color0;Color 0;9;0;Create;True;0,0,0,0;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DepthFade;164;-1363.74,813.0583;Float;False;True;1;0;FLOAT;1.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;161;-1653.469,421.059;Float;False;Property;_FoamColor;Foam Color;3;0;Create;True;1,1,1,0;0,0.08823532,0.08458419,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;213;-1662.766,-634.5618;Float;True;Property;_TextureSample1;Texture Sample 1;10;0;Create;True;None;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;234;-2215.95,-314.0596;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;218;-1985.796,-441.6866;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;55,55;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;236;-2159.977,-156.2715;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.6;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;209;-1669.594,-827.3258;Float;True;Property;_TextureSample0;Texture Sample 0;11;0;Create;True;None;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;227;-1182.919,918.7961;Float;False;Property;_WaterDepth;Water Depth;11;0;Create;True;0;16.9;0;55;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;157;-1130.62,790.3007;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0.0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;232;-2023.349,-237.1152;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;55,55;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;217;-1664.078,-438.6591;Float;True;Property;_TextureSample2;Texture Sample 2;9;0;Create;True;None;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.BlendNormalsNode;215;-1268.572,-505.1681;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;240;-2073.657,7.834746;Float;False;Property;_Bump2Scale;Bump2 Scale;10;0;Create;True;1;0.41;0;3;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-1174.175,524.8026;Float;False;Property;_Opacity;Opacity;5;0;Create;True;0.5;0.8;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.DepthFade;226;-819.5098,906.0258;Float;False;True;1;0;FLOAT;1.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BlendNormalsNode;222;-993.381,-461.7857;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;235;-1670.811,-246.0074;Float;True;Property;_TextureSample3;Texture Sample 3;9;0;Create;True;None;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;2;-1110.903,73.51465;Float;False;Property;_WaterColor;Water Color;0;0;Create;True;0.4926471,0.8740366,1,1;0.2100454,0.2720588,0.2386999,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;191;-892.1213,746.2888;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TFHCRemapNode;230;-807.7338,436.9832;Float;False;5;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;3;FLOAT;-1.0;False;4;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;231;-807.7339,590.9341;Float;False;5;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;3;FLOAT;1.0;False;4;FLOAT;2.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;228;-589.2574,805.2882;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;1.0;False;2;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;243;-629.699,206.717;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.BlendNormalsNode;237;-821.1271,-164.5241;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TFHCRemapNode;229;-619.2225,480.9694;Float;False;5;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;3;FLOAT;0.0;False;4;FLOAT;1.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NormalizeNode;225;-627.4688,-58.01003;Float;False;1;0;FLOAT3;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;47;-2505.51,-197.1212;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.FresnelNode;241;-280.5645,-217.3252;Float;False;Tangent;4;0;FLOAT3;0,0,0;False;1;FLOAT;0.02;False;2;FLOAT;1.0;False;3;FLOAT;5.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;53;-2904.687,-276.0045;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;207;-992.4531,367.7151;Float;False;Property;_Metallic;Metallic;7;0;Create;True;0;0.083;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;245;-364.8171,573.9686;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;206;-997.2556,270.1191;Float;False;Property;_Gloss;Gloss;6;0;Create;True;0;0.756;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;118;-2705.925,-229.0394;Float;False;False;True;False;True;1;0;FLOAT3;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;244;-487.0902,153.0206;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;-222.1961,219.2795;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;MK4/Simple Water;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;Off;0;3;False;0;0;False;0;Transparent;0.5;True;False;0;False;Transparent;Transparent;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;0;4;10;25;False;0.5;False;2;SrcAlpha;OneMinusSrcAlpha;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;88;0;89;0
WireConnection;88;1;15;0
WireConnection;214;0;89;2
WireConnection;214;1;15;0
WireConnection;239;0;216;0
WireConnection;212;0;216;0
WireConnection;212;1;214;0
WireConnection;221;0;216;0
WireConnection;96;0;239;0
WireConnection;96;1;88;0
WireConnection;233;0;214;0
WireConnection;220;0;88;0
WireConnection;164;0;167;0
WireConnection;213;0;223;0
WireConnection;213;1;212;0
WireConnection;213;5;224;0
WireConnection;234;0;216;0
WireConnection;218;0;221;0
WireConnection;218;1;220;0
WireConnection;236;0;233;0
WireConnection;209;0;223;0
WireConnection;209;1;96;0
WireConnection;209;5;224;0
WireConnection;157;0;161;0
WireConnection;157;1;159;0
WireConnection;157;2;164;0
WireConnection;232;0;234;0
WireConnection;232;1;236;0
WireConnection;217;0;223;0
WireConnection;217;1;218;0
WireConnection;217;5;224;0
WireConnection;215;0;209;0
WireConnection;215;1;213;0
WireConnection;226;0;227;0
WireConnection;222;0;215;0
WireConnection;222;1;217;0
WireConnection;235;0;223;0
WireConnection;235;1;232;0
WireConnection;235;5;240;0
WireConnection;191;0;157;0
WireConnection;230;0;11;0
WireConnection;231;0;11;0
WireConnection;228;2;226;0
WireConnection;243;0;191;0
WireConnection;243;1;2;0
WireConnection;237;0;222;0
WireConnection;237;1;235;0
WireConnection;229;0;228;0
WireConnection;229;3;230;0
WireConnection;229;4;231;0
WireConnection;225;0;237;0
WireConnection;47;0;88;0
WireConnection;47;1;118;0
WireConnection;241;0;225;0
WireConnection;245;0;229;0
WireConnection;118;0;53;0
WireConnection;244;0;243;0
WireConnection;0;0;244;0
WireConnection;0;1;225;0
WireConnection;0;3;207;0
WireConnection;0;4;206;0
WireConnection;0;9;245;0
ASEEND*/
//CHKSM=FA735F541AFFE5F702D700B1CE61103BF79BEE31