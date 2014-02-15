#version 410 core

uniform samplerCube tex_cubemap;

const vec3 ambient = vec3(0.1, 0.1, 0.1);
const vec3 lightVecNormalized = normalize(vec3(0.5, 0.5, 2.0));
const vec3 lightColor = vec3(1.0, 0.2, 0.2);

in VS_OUT
{
    vec3 normal;
    vec3 view;
} fs_in;

out vec4 color;

void main(void)
{
    // Reflect view vector about the plane defined by the normal
    // at the fragment
    vec3 r = reflect(fs_in.view, normalize(fs_in.normal));

    // Sample from scaled using reflection vector
     color = texture(tex_cubemap, r);

	float diffuse = clamp(dot(lightVecNormalized, normalize(fs_in.normal)), 0.0, 1.0);

	color = color * vec4(ambient + diffuse * lightColor, 1.0);
}
