struct PSInput
{
	float4 Position: SV_POSITION;
	float4 Color: COLOR0;
};

float4 Main(PSInput input) : SV_TARGET
{
	return input.Color;
}