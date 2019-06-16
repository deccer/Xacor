#version 430 core

out gl_PerVertex
{ 
    vec4 gl_Position; 
};

layout (location = 0) in vec3 i_position;
layout (location = 0) in vec2 i_uv;
  
out vec4 o_vertex_color;
out vec2 o_vertex_uv;

void main()
{
    gl_Position = vec4(i_position, 1.0);
    o_vertex_color = vec4(0.5, 0.0, 0.0, 1.0);
	o_vertex_uv = i_uv;
}