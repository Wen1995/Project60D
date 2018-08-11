// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: commented out 'float3 _WorldSpaceCameraPos', a built-in variable

Shader "Custom/VolumeFog" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_voidRect ("VoidRect", Vector) = (1,1,1,1)
		_fogColor ("FogColor", Color) = (1,1,1,1)
		_fogHeight ("FogHeight", Range(0, 100)) = 5
		_thickness ("Thickness", Range(0, 1)) = 0.5
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		Blend one one

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float3 viewDir;
			float4 pos;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		float _fogNear;
		float _fogFar;
		float4 _fogColor;
		float _fogHeight;
		float _thickness;
		sampler2D _CameraDepthTexture;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void vert (inout appdata_full v, out Input o)
		{
			o.pos = mul(unity_ObjectToWorld, v.vertex);
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			//获得摄像机到顶点的射线参数方程
			//求得与雾平面的交点
			//计算交点到顶点的距离
			//计算雾浓度因子
			// Albedo comes from a texture tinted by color
			// float camera2fogDistance = (_WorldSpaceCameraPos.y - _fogHeight) / viewDir.y;
			// float3 cameraFogIntersection = _WorldSpaceCameraPos + viewDir * camera2fogDistance;
			//float distance = distance(IN.pos, camera)
			float distance = _fogHeight / IN.viewDir.y;
			float cameraDistance = _WorldSpaceCameraPos.y / IN.viewDir.y;
			// float d = length(_WorldSpaceCameraPos.xyz - IN.pos);
			// float d = length(IN.viewDir);
			// float t = saturate(d);
			//float t = saturate((d - _fogNear) / (_fogFar - _fogNear) / 50);
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = _fogColor * _thickness;
			//o.Albedo = lerp(float3(1,1,1), _fogColor, saturate(distance - _fogHeight / _fogHeight * 2));
			//o.Albedo = _fogColor * 20 / distance + 20;
			//o.Albedo = lerp(c.rbg, _fogColor, 20 / distance + 20);
			// Metallic and smoothness come from slider variables
			// o.Metallic = _Metallic;
			// o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
