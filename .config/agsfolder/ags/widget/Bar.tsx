import app from "ags/gtk4/app"
import GLib from "gi://GLib"
import Astal from "gi://Astal?version=4.0"
import Gtk from "gi://Gtk?version=4.0"
import Gdk from "gi://Gdk?version=4.0"
import AstalBattery from "gi://AstalBattery"
import AstalPowerProfiles from "gi://AstalPowerProfiles"
import AstalWp from "gi://AstalWp"
import AstalNetwork from "gi://AstalNetwork"
import AstalTray from "gi://AstalTray"
import AstalMpris from "gi://AstalMpris"
import AstalApps from "gi://AstalApps"
import AstalHyprland from "gi://AstalHyprland"
import { For, With, createBinding, onCleanup } from "ags"
import { createPoll } from "ags/time"
import { execAsync } from "ags/process"

function Workspaces() {
  const hypr = AstalHyprland.get_default()
  const workspaces = createBinding(hypr, "workspaces")
  const activeWorkspace = createBinding(hypr, "focusedWorkspace")

  const sortWorkspaces = (list: Array<AstalHyprland.Workspace> | null) => {
    if (!list) return []
    return [...list].sort((a, b) => a.id - b.id)
  }

  return (
    <box cssClasses={["workspaces"]} spacing={8}>
      <For each={workspaces(sortWorkspaces)}>
        {(ws) => {
          const isActive = activeWorkspace((active) => active?.id === ws.id)
          return (
            <button
              onClicked={() => ws.focus()}
              cssClasses={isActive((active) => active ? ["active"] : [])}
            >
              <label label={String(ws.id)} />
            </button>
          )
        }}
      </For>
    </box>
  )
}

function Mpris() {
  const mpris = AstalMpris.get_default()
  const apps = new AstalApps.Apps()
  const players = createBinding(mpris, "players")

  return (
    <menubutton cssClasses={["mpris-mod"]}>
      <box>
        <For each={players}>
          {(player) => {
            const [app] = apps.exact_query(player.entry)
            return <image visible={!!app.iconName} iconName={app?.iconName} />
          }}
        </For>
      </box>
      <popover>
        <box spacing={4} orientation={Gtk.Orientation.VERTICAL}>
          <For each={players}>
            {(player) => (
              <box spacing={4} widthRequest={200}>
                <box overflow={Gtk.Overflow.HIDDEN} css="border-radius: 8px;">
                  <image pixelSize={64} file={createBinding(player, "coverArt")} />
                </box>
                <box valign={Gtk.Align.CENTER} orientation={Gtk.Orientation.VERTICAL}>
                  <label xalign={0} label={createBinding(player, "title")} />
                  <label xalign={0} label={createBinding(player, "artist")} />
                </box>
                <box hexpand halign={Gtk.Align.END}>
                  <button onClicked={() => player.previous()} visible={createBinding(player, "canGoPrevious")}>
                    <image iconName="media-seek-backward-symbolic" />
                  </button>
                  <button onClicked={() => player.play_pause()} visible={createBinding(player, "canControl")}>
                    <box>
                      <image iconName="media-playback-start-symbolic" visible={createBinding(player, "playbackStatus")((s) => s === AstalMpris.PlaybackStatus.PLAYING)} />
                      <image iconName="media-playback-pause-symbolic" visible={createBinding(player, "playbackStatus")((s) => s !== AstalMpris.PlaybackStatus.PLAYING)} />
                    </box>
                  </button>
                  <button onClicked={() => player.next()} visible={createBinding(player, "canGoNext")}>
                    <image iconName="media-seek-forward-symbolic" />
                  </button>
                </box>
              </box>
            )}
          </For>
        </box>
      </popover>
    </menubutton>
  )
}

function Tray() {
  const tray = AstalTray.get_default()
  const items = createBinding(tray, "items")

  const init = (btn: Gtk.MenuButton, item: AstalTray.TrayItem) => {
    btn.menuModel = item.menuModel
    btn.insert_action_group("dbusmenu", item.actionGroup)
    item.connect("notify::action-group", () => {
      btn.insert_action_group("dbusmenu", item.actionGroup)
    })
  }

  return (
    <box cssClasses={["tray-mod"]}>
      <For each={items}>
        {(item) => (
          <menubutton $={(self) => init(self, item)}>
            <image gicon={createBinding(item, "gicon")} />
          </menubutton>
        )}
      </For>
    </box>
  )
}

function Wireless() {
  const network = AstalNetwork.get_default()
  const wifi = createBinding(network, "wifi")

  const sorted = (arr: Array<AstalNetwork.AccessPoint>) => {
    return arr.filter((ap) => !!ap.ssid).sort((a, b) => b.strength - a.strength)
  }

  async function connect(ap: AstalNetwork.AccessPoint) {
    try { await execAsync(`nmcli d wifi connect ${ap.bssid}`) } catch (e) { console.error(e) }
  }

  return (
    <box visible={wifi(Boolean)}>
      <With value={wifi}>
        {(wifi) => wifi && (
          <menubutton cssClasses={["wifi-mod"]}>
            <image iconName={createBinding(wifi, "iconName")} />
            <popover>
              <box orientation={Gtk.Orientation.VERTICAL}>
                <For each={createBinding(wifi, "accessPoints")(sorted)}>
                  {(ap: AstalNetwork.AccessPoint) => (
                    <button onClicked={() => connect(ap)}>
                      <box spacing={4}>
                        <image iconName={createBinding(ap, "iconName")} />
                        <label label={createBinding(ap, "ssid")} />
                        <image iconName="object-select-symbolic" visible={createBinding(wifi, "activeAccessPoint")((active) => active === ap)} />
                      </box>
                    </button>
                  )}
                </For>
              </box>
            </popover>
          </menubutton>
        )}
      </With>
    </box>
  )
}

function AudioOutput() {
  const { defaultSpeaker: speaker } = AstalWp.get_default()!

  return (
    <menubutton cssClasses={["audio-mod"]}>
      <box>
        <image iconName={createBinding(speaker, "volumeIcon")} />
      </box>
      <popover>
        <box>
          <slider widthRequest={260} onChangeValue={({ value }) => speaker.set_volume(value)} value={createBinding(speaker, "volume")} />
        </box>
      </popover>
    </menubutton>
  )
}

function Battery() {
  const battery = AstalBattery.get_default()
  const powerprofiles = AstalPowerProfiles.get_default()
  const percent = createBinding(battery, "percentage")((p) => `${Math.floor(p * 100)}%`)

  return (
    <menubutton visible={createBinding(battery, "isPresent")} cssClasses={["battery-mod"]}>
      <box spacing={4}>
        <image iconName={createBinding(battery, "iconName")} />
        <label label={percent} />
      </box>
      <popover>
        <box orientation={Gtk.Orientation.VERTICAL}>
          {powerprofiles.get_profiles().map(({ profile }) => (
            <button onClicked={() => powerprofiles.set_active_profile(profile)}>
              <label label={profile} xalign={0} />
            </button>
          ))}
        </box>
      </popover>
    </menubutton>
  )
}

function Clock({ format = "%H:%M" }) {
  const time = createPoll("", 1000, () => GLib.DateTime.new_now_local().format(format)!)

  return (
    <menubutton cssClasses={["clock-mod"]}>
      <box>
        <label label={time} />
      </box>
      <popover>
        <Gtk.Calendar />
      </popover>
    </menubutton>
  )
}

export default function Bar({ gdkmonitor }) {
  let win: Astal.Window
  const { TOP, LEFT, RIGHT } = Astal.WindowAnchor

  onCleanup(() => {
    win.destroy()
  })

  return (
    <window
      $={(self) => (win = self)}
      visible
      namespace="my-bar" // Conecta directamente con 'window.my-bar' en tu SCSS
      name={`bar-${gdkmonitor.connector}`}
      gdkmonitor={gdkmonitor}
      exclusivity={Astal.Exclusivity.EXCLUSIVE}
      anchor={TOP | LEFT | RIGHT}
      application={app}
      marginTop={5}
    >
      <centerbox heightRequest={35}>
        <box $type="start" spacing={8} cssClasses={["mod-izq"]}>
          <Workspaces />
        </box>
        <box $type="center" cssClasses={["mod-cen"]}>
          <Clock />
        </box>
        <box $type="end" spacing={4} cssClasses={["mod-der"]}>
          <Mpris />
          <Tray />
          <Wireless />
          <AudioOutput />
          <Battery />
        </box>
      </centerbox>
    </window>
  )
}

app.start({
  instanceName: "ags", 

  requestHandler(request, res) {
    const requestStr = String(request);
    const cleanRequest = requestStr.replace(/['"()]/g, "").trim();

    if (cleanRequest === "rc") {
      try {
        const cssPath = "/home/david/dotfiles/.config/agsfolder/ags/style.css";
        
        // TRUCO NATIVO PARA GTK4: Forzamos la relectura reiniciando el proveedor de estilos
        // Al aplicar un string vacío primero, GTK4 se ve obligado a liberar la caché del archivo anterior
        app.apply_css(""); 
        app.apply_css(cssPath);
        
        res("Barra AGS actualizada en vivo con éxito");
      } catch (error) {
        console.error("Error al recargar el CSS en vivo:", error);
        res("Error interno al recargar los estilos");
      }
    } else {
      res(`Petición no reconocida: Evaluado como [${cleanRequest}]`);
    }
  },

  main() {
    try {
      app.apply_css("/home/david/dotfiles/.config/agsfolder/ags/style.css") 
    } catch (error) {
      console.error("Error cargando el archivo CSS nativo:", error)
    }

    const display = Gdk.Display.get_default()
    const monitors = display ? display.get_monitors() : null
    if (monitors && monitors.get_n_items() > 0) {
      const monitor = monitors.get_item(0) as Gdk.Monitor
      Bar({ gdkmonitor: monitor })
    }
  },
})
