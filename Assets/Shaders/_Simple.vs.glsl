#version 430 core

out gl_PerVertex
{ 
    vec4 gl_Position; 
};

layout (location = 0) in vec3 i_position;
layout (location = 0) in vec3 i_color;
  
out vec4 o_vertex_color;

void main()
{
    gl_Position = vec4(i_position, 1.0);
    o_vertex_color = vec4(i_color, 1.0);
}