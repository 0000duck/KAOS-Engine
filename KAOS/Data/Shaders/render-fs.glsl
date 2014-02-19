#version 410 core

uniform sampler1D tex;
uniform vec2 center;
uniform float scale;
uniform int iter;

uniform samplerCube tex_cubemap;

const vec3 ambient = vec3(0.1, 0.1, 0.1);
const vec3 lightVecNormalized = normalize(vec3(0.5, 0.5, 2.0));
const vec3 lightColor = vec3(0.8, 0.2, 0.8);

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

	// see: http://nuclear.mutantstargoat.com/articles/sdr_fract/

	vec2 z, c;

    //c.x = 1.3333 * (gl_TexCoord[0].x - 0.5) * scale - center.x;
    //c.y = (gl_TexCoord[0].y - 0.5) * scale - center.y;

    int i;
    z = c;
    for(i=0; i<iter; i++) {
        float x = (z.x * z.x - z.y * z.y) + c.x;
        float y = (z.y * z.x + z.x * z.y) + c.y;

        if((x * x + y * y) > 4.0) break;
        z.x = x;
        z.y = y;
    }

    //gl_FragColor = texture1D(tex, (i == iter ? 0.0 : float(i)) / 100.0);
}