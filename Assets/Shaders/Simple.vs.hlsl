struct VSInput
{
	float3 Position: POSITION;
	float4 Color: COLOR0;
};

struct PSInput
{
	float4 Position: SV_POSITION;
	float4 Color: COLOR0;
};

cbuffer Input
{
	float4x4 M_World;
	float4x4 M_View;
	float4x4 M_Projection;
};

PSInput Main(VSInput input)
{
	PSInput output = (PSInput)0;
	output.Position = mul(float4(input.Position, 1.0f), M_World);
	output.Position = mul(output.Position, M_View);
	output.Position = mul(output.Position, M_Projection);
	output.Color = input.Color;
	return output;
}