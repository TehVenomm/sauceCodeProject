package net.gogame.gowrap.model.configuration;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import net.gogame.gowrap.support.JSONUtils;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

public class Configuration {
    private Integrations integrations;
    private Settings settings;

    public static class Integrations {
        private Core core;

        public static class Core {
            private Map<String, LocaleConfiguration> locales;
            private Boolean slideIn;
            private Boolean slideOut;
            private List<String> supportedLocales;

            public static class LocaleConfiguration {
                private String facebookUrl;
                private String forumUrl;
                private String instagramUrl;
                private String twitterUrl;
                private String whatsNewUrl;
                private String wikiUrl;
                private String youtubeUrl;

                public LocaleConfiguration() {
                }

                public LocaleConfiguration(JSONObject jSONObject) throws JSONException {
                    setWhatsNewUrl(JSONUtils.optUrl(jSONObject, "whatsNewUrl"));
                    setFacebookUrl(JSONUtils.optUrl(jSONObject, "facebookUrl"));
                    setTwitterUrl(JSONUtils.optUrl(jSONObject, "twitterUrl"));
                    setInstagramUrl(JSONUtils.optUrl(jSONObject, "instagramUrl"));
                    setYoutubeUrl(JSONUtils.optUrl(jSONObject, "youtubeUrl"));
                    setForumUrl(JSONUtils.optUrl(jSONObject, "forumUrl"));
                    setWikiUrl(JSONUtils.optUrl(jSONObject, "wikiUrl"));
                }

                public String getWhatsNewUrl() {
                    return this.whatsNewUrl;
                }

                public void setWhatsNewUrl(String str) {
                    this.whatsNewUrl = str;
                }

                public String getFacebookUrl() {
                    return this.facebookUrl;
                }

                public void setFacebookUrl(String str) {
                    this.facebookUrl = str;
                }

                public String getTwitterUrl() {
                    return this.twitterUrl;
                }

                public void setTwitterUrl(String str) {
                    this.twitterUrl = str;
                }

                public String getInstagramUrl() {
                    return this.instagramUrl;
                }

                public void setInstagramUrl(String str) {
                    this.instagramUrl = str;
                }

                public String getYoutubeUrl() {
                    return this.youtubeUrl;
                }

                public void setYoutubeUrl(String str) {
                    this.youtubeUrl = str;
                }

                public String getForumUrl() {
                    return this.forumUrl;
                }

                public void setForumUrl(String str) {
                    this.forumUrl = str;
                }

                public String getWikiUrl() {
                    return this.wikiUrl;
                }

                public void setWikiUrl(String str) {
                    this.wikiUrl = str;
                }
            }

            public Core() {
            }

            public Core(JSONObject jSONObject) throws JSONException {
                if (jSONObject.has("locales")) {
                    JSONObject optJSONObject = jSONObject.optJSONObject("locales");
                    if (optJSONObject != null) {
                        this.locales = new HashMap();
                        Iterator keys = optJSONObject.keys();
                        while (keys.hasNext()) {
                            String str = (String) keys.next();
                            JSONObject optJSONObject2 = optJSONObject.optJSONObject(str);
                            if (optJSONObject2 != null) {
                                this.locales.put(str, new LocaleConfiguration(optJSONObject2));
                            } else {
                                this.locales.put(str, null);
                            }
                        }
                    }
                }
                if (jSONObject.has("supportedLocales")) {
                    JSONArray optJSONArray = jSONObject.optJSONArray("supportedLocales");
                    if (optJSONArray != null) {
                        this.supportedLocales = new ArrayList();
                        for (int i = 0; i < optJSONArray.length(); i++) {
                            this.supportedLocales.add(optJSONArray.getString(i));
                        }
                    }
                }
                this.slideOut = JSONUtils.optBoolean(jSONObject, "slideOut");
                this.slideIn = JSONUtils.optBoolean(jSONObject, "slideIn");
            }

            public Map<String, LocaleConfiguration> getLocales() {
                return this.locales;
            }

            public void setLocales(Map<String, LocaleConfiguration> map) {
                this.locales = map;
            }

            public List<String> getSupportedLocales() {
                return this.supportedLocales;
            }

            public void setSupportedLocales(List<String> list) {
                this.supportedLocales = list;
            }

            public Boolean getSlideOut() {
                return this.slideOut;
            }

            public void setSlideOut(Boolean bool) {
                this.slideOut = bool;
            }

            public Boolean getSlideIn() {
                return this.slideIn;
            }

            public void setSlideIn(Boolean bool) {
                this.slideIn = bool;
            }
        }

        public Integrations() {
        }

        public Integrations(JSONObject jSONObject) throws JSONException {
            if (jSONObject.has("core")) {
                JSONObject optJSONObject = jSONObject.optJSONObject("core");
                if (optJSONObject != null) {
                    this.core = new Core(optJSONObject);
                }
            }
        }

        public Core getCore() {
            return this.core;
        }

        public void setCore(Core core2) {
            this.core = core2;
        }
    }

    public static class Settings {
        private Boolean chatBotEnabled;
        private String newsWidgetVersion;

        public Settings() {
        }

        public Settings(JSONObject jSONObject) throws JSONException {
            this.chatBotEnabled = JSONUtils.optBoolean(jSONObject, "chatBotEnabled");
            this.newsWidgetVersion = JSONUtils.optString(jSONObject, "newsWidgetVersion");
        }

        public Boolean getChatBotEnabled() {
            return this.chatBotEnabled;
        }

        public void setChatBotEnabled(Boolean bool) {
            this.chatBotEnabled = bool;
        }

        public String getNewsWidgetVersion() {
            return this.newsWidgetVersion;
        }

        public void setNewsWidgetVersion(String str) {
            this.newsWidgetVersion = str;
        }
    }

    public Configuration() {
    }

    public Configuration(JSONObject jSONObject) throws JSONException {
        if (jSONObject != null) {
            if (jSONObject.has("integrations")) {
                JSONObject optJSONObject = jSONObject.optJSONObject("integrations");
                if (optJSONObject != null) {
                    this.integrations = new Integrations(optJSONObject);
                }
            }
            if (jSONObject.has("settings")) {
                JSONObject optJSONObject2 = jSONObject.optJSONObject("settings");
                if (optJSONObject2 != null) {
                    this.settings = new Settings(optJSONObject2);
                }
            }
        }
    }

    public Integrations getIntegrations() {
        return this.integrations;
    }

    public void setIntegrations(Integrations integrations2) {
        this.integrations = integrations2;
    }

    public Settings getSettings() {
        return this.settings;
    }

    public void setSettings(Settings settings2) {
        this.settings = settings2;
    }
}
