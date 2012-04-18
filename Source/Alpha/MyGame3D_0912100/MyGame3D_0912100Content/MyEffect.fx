float4x4 World;
float4x4 View;
float4x4 Projection;
texture Texture;
// TODO: add effect parameters here.
sampler Sampler = sampler_state
{
	texture = <Texture>;
	   MinFilter = Anisotropic; // Minification Filter
   MagFilter =  Anisotropic; // Magnification Filter
   MipFilter = linear; // Mip-mapping
   AddressU = clamp; // Address Mode for U Coordinates
   AddressV = clamp; // Address Mode for V Coordinates
};



struct VertexShaderInput
{
    float4 Position : POSITION0;
	float2 UV : TEXCOORD0;
    // TODO: add input channels such as texture
    // coordinates and vertex colors here.
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
	float2 UV : TEXCOORD0;
    // TODO: add vertex shader outputs such as colors and texture
    // coordinates here. These values will automatically be interpolated
    // over the triangle, and provided as input to your pixel shader.
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);
	output.UV = input.UV;
    // TODO: add your vertex shader code here.

    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
    // TODO: add your pixel shader code here.
	float4 Color;
	
    Color = tex2D(Sampler, input.UV);
	float temp1  = Color.r;
	float temp2 = Color.g;
	float temp3 = Color.b;
	if(temp1 ==  0 && temp2 == 0 && temp3 == 0){
	Color.a = 0;
	}
    return Color;
}

technique Technique1
{
    pass Pass1
    {
        // TODO: set renderstates here.
		AlphaBlendEnable = TRUE;
        DestBlend = INVSRCALPHA;
        SrcBlend = SRCALPHA;
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
