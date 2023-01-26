import Service from "./Service";

export default class SettingsService extends Service
{
    async getSocketSettings() {
        return await this.get({ url: '/settings/socket' });
    }
}