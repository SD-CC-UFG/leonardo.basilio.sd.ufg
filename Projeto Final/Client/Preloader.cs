using System;
using System.Runtime.InteropServices;
using Gdk;

namespace Client {

    [System.ComponentModel.ToolboxItem(true)]
    public partial class Preloader : Gtk.Bin {

        [DllImport("libgdk_pixbuf-2.0.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern void gdk_pixbuf_simple_anim_set_loop(IntPtr raw, bool loop);

        public Preloader() {

            this.Build();

            var animation = new PixbufSimpleAnim(16, 16, 12);
            var sprites = Pixbuf.LoadFromResource("Client.Resources.Preloader");

            for (var i = 0; i < 8; i++) {

                animation.AddFrame(new Pixbuf(sprites, i * 16, 0, 16, 16));

            }

            gdk_pixbuf_simple_anim_set_loop(animation.Handle, true);

            imgAnimation.Animation = animation;

        }

    }

}
