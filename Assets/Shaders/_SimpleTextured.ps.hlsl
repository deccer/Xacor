Texture2D T_Texture;

SamplerState S_Texture;

struct PSInput
{
	float4 Position: SV_POSITION;
	float2 Uv: TEXCOORD0;
};

float4 Main(PSInput input) : SV_TARGET
{
	float3 color = T_Texture.Sample(S_Texture, input.Uv);
	return float4(color, 1.0f);
}