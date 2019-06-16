#version 430 core

out vec4 o_frag_color;
  
in vec4 o_vertex_color;

void main()
{
    o_frag_color = o_vertex_color;
} 