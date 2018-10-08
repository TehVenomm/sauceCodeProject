package net.gogame.gowrap.model.configuration;

import java.util.List;
import java.util.Map;
import net.gogame.gowrap.support.JSONUtils;
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

                public LocaleConfiguration(org.json.JSONObject r2) throws org.json.JSONException {
                    /* JADX: method processing error */
/*
Error: java.lang.NullPointerException
	at jadx.core.dex.visitors.regions.ProcessVariables.addToUsageMap(ProcessVariables.java:284)
	at jadx.core.dex.visitors.regions.ProcessVariables.visit(ProcessVariables.java:182)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:31)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:17)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
	at jadx.core.ProcessClass.process(ProcessClass.java:34)
	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:282)
	at jadx.api.JavaClass.decompile(JavaClass.java:62)
	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:200)
	at jadx.api.JadxDecompiler$$Lambda$8/1758893871.run(Unknown Source)
*/
                    /*
                    r1 = this;
                    r1.<init>();
                    r0 = "whatsNewUrl";
                    r0 = net.gogame.gowrap.support.JSONUtils.optUrl(r2, r0);
                    r1.setWhatsNewUrl(r0);
                    r0 = "facebookUrl";
                    r0 = net.gogame.gowrap.support.JSONUtils.optUrl(r2, r0);
                    r1.setFacebookUrl(r0);
                    r0 = "twitterUrl";
                    r0 = net.gogame.gowrap.support.JSONUtils.optUrl(r2, r0);
                    r1.setTwitterUrl(r0);
                    r0 = "instagramUrl";
                    r0 = net.gogame.gowrap.support.JSONUtils.optUrl(r2, r0);
                    r1.setInstagramUrl(r0);
                    r0 = "youtubeUrl";
                    r0 = net.gogame.gowrap.support.JSONUtils.optUrl(r2, r0);
                    r1.setYoutubeUrl(r0);
                    r0 = "forumUrl";
                    r0 = net.gogame.gowrap.support.JSONUtils.optUrl(r2, r0);
                    r1.setForumUrl(r0);
                    r0 = "wikiUrl";
                    r0 = net.gogame.gowrap.support.JSONUtils.optUrl(r2, r0);
                    r1.setWikiUrl(r0);
                    return;
                    */
                    throw new UnsupportedOperationException("Method not decompiled: net.gogame.gowrap.model.configuration.Configuration.Integrations.Core.LocaleConfiguration.<init>(org.json.JSONObject):void");
                }

                public java.lang.String getWhatsNewUrl() {
                    /* JADX: method processing error */
/*
Error: jadx.core.utils.exceptions.JadxRuntimeException: SSA rename variables already executed
	at jadx.core.dex.visitors.ssa.SSATransform.renameVariables(SSATransform.java:120)
	at jadx.core.dex.visitors.ssa.SSATransform.process(SSATransform.java:52)
	at jadx.core.dex.visitors.ssa.SSATransform.visit(SSATransform.java:42)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:31)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:17)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
	at jadx.core.ProcessClass.process(ProcessClass.java:34)
	at jadx.core.ProcessClass.processDependencies(ProcessClass.java:56)
	at jadx.core.ProcessClass.process(ProcessClass.java:39)
	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:282)
	at jadx.api.JavaClass.decompile(JavaClass.java:62)
	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:200)
	at jadx.api.JadxDecompiler$$Lambda$8/1758893871.run(Unknown Source)
*/
                    /*
                    r1 = this;
                    r0 = r1.whatsNewUrl;
                    return r0;
                    */
                    throw new UnsupportedOperationException("Method not decompiled: net.gogame.gowrap.model.configuration.Configuration.Integrations.Core.LocaleConfiguration.getWhatsNewUrl():java.lang.String");
                }

                public void setWhatsNewUrl(java.lang.String r1) {
                    /* JADX: method processing error */
/*
Error: jadx.core.utils.exceptions.JadxRuntimeException: SSA rename variables already executed
	at jadx.core.dex.visitors.ssa.SSATransform.renameVariables(SSATransform.java:120)
	at jadx.core.dex.visitors.ssa.SSATransform.process(SSATransform.java:52)
	at jadx.core.dex.visitors.ssa.SSATransform.visit(SSATransform.java:42)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:31)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:17)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
	at jadx.core.ProcessClass.process(ProcessClass.java:34)
	at jadx.core.ProcessClass.processDependencies(ProcessClass.java:56)
	at jadx.core.ProcessClass.process(ProcessClass.java:39)
	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:282)
	at jadx.api.JavaClass.decompile(JavaClass.java:62)
	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:200)
	at jadx.api.JadxDecompiler$$Lambda$8/1758893871.run(Unknown Source)
*/
                    /*
                    r0 = this;
                    r0.whatsNewUrl = r1;
                    return;
                    */
                    throw new UnsupportedOperationException("Method not decompiled: net.gogame.gowrap.model.configuration.Configuration.Integrations.Core.LocaleConfiguration.setWhatsNewUrl(java.lang.String):void");
                }

                public java.lang.String getFacebookUrl() {
                    /* JADX: method processing error */
/*
Error: jadx.core.utils.exceptions.JadxRuntimeException: SSA rename variables already executed
	at jadx.core.dex.visitors.ssa.SSATransform.renameVariables(SSATransform.java:120)
	at jadx.core.dex.visitors.ssa.SSATransform.process(SSATransform.java:52)
	at jadx.core.dex.visitors.ssa.SSATransform.visit(SSATransform.java:42)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:31)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:17)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
	at jadx.core.ProcessClass.process(ProcessClass.java:34)
	at jadx.core.ProcessClass.processDependencies(ProcessClass.java:56)
	at jadx.core.ProcessClass.process(ProcessClass.java:39)
	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:282)
	at jadx.api.JavaClass.decompile(JavaClass.java:62)
	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:200)
	at jadx.api.JadxDecompiler$$Lambda$8/1758893871.run(Unknown Source)
*/
                    /*
                    r1 = this;
                    r0 = r1.facebookUrl;
                    return r0;
                    */
                    throw new UnsupportedOperationException("Method not decompiled: net.gogame.gowrap.model.configuration.Configuration.Integrations.Core.LocaleConfiguration.getFacebookUrl():java.lang.String");
                }

                public void setFacebookUrl(java.lang.String r1) {
                    /* JADX: method processing error */
/*
Error: jadx.core.utils.exceptions.JadxRuntimeException: SSA rename variables already executed
	at jadx.core.dex.visitors.ssa.SSATransform.renameVariables(SSATransform.java:120)
	at jadx.core.dex.visitors.ssa.SSATransform.process(SSATransform.java:52)
	at jadx.core.dex.visitors.ssa.SSATransform.visit(SSATransform.java:42)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:31)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:17)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
	at jadx.core.ProcessClass.process(ProcessClass.java:34)
	at jadx.core.ProcessClass.processDependencies(ProcessClass.java:56)
	at jadx.core.ProcessClass.process(ProcessClass.java:39)
	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:282)
	at jadx.api.JavaClass.decompile(JavaClass.java:62)
	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:200)
	at jadx.api.JadxDecompiler$$Lambda$8/1758893871.run(Unknown Source)
*/
                    /*
                    r0 = this;
                    r0.facebookUrl = r1;
                    return;
                    */
                    throw new UnsupportedOperationException("Method not decompiled: net.gogame.gowrap.model.configuration.Configuration.Integrations.Core.LocaleConfiguration.setFacebookUrl(java.lang.String):void");
                }

                public java.lang.String getTwitterUrl() {
                    /* JADX: method processing error */
/*
Error: jadx.core.utils.exceptions.JadxRuntimeException: SSA rename variables already executed
	at jadx.core.dex.visitors.ssa.SSATransform.renameVariables(SSATransform.java:120)
	at jadx.core.dex.visitors.ssa.SSATransform.process(SSATransform.java:52)
	at jadx.core.dex.visitors.ssa.SSATransform.visit(SSATransform.java:42)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:31)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:17)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
	at jadx.core.ProcessClass.process(ProcessClass.java:34)
	at jadx.core.ProcessClass.processDependencies(ProcessClass.java:56)
	at jadx.core.ProcessClass.process(ProcessClass.java:39)
	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:282)
	at jadx.api.JavaClass.decompile(JavaClass.java:62)
	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:200)
	at jadx.api.JadxDecompiler$$Lambda$8/1758893871.run(Unknown Source)
*/
                    /*
                    r1 = this;
                    r0 = r1.twitterUrl;
                    return r0;
                    */
                    throw new UnsupportedOperationException("Method not decompiled: net.gogame.gowrap.model.configuration.Configuration.Integrations.Core.LocaleConfiguration.getTwitterUrl():java.lang.String");
                }

                public void setTwitterUrl(java.lang.String r1) {
                    /* JADX: method processing error */
/*
Error: jadx.core.utils.exceptions.JadxRuntimeException: SSA rename variables already executed
	at jadx.core.dex.visitors.ssa.SSATransform.renameVariables(SSATransform.java:120)
	at jadx.core.dex.visitors.ssa.SSATransform.process(SSATransform.java:52)
	at jadx.core.dex.visitors.ssa.SSATransform.visit(SSATransform.java:42)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:31)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:17)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
	at jadx.core.ProcessClass.process(ProcessClass.java:34)
	at jadx.core.ProcessClass.processDependencies(ProcessClass.java:56)
	at jadx.core.ProcessClass.process(ProcessClass.java:39)
	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:282)
	at jadx.api.JavaClass.decompile(JavaClass.java:62)
	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:200)
	at jadx.api.JadxDecompiler$$Lambda$8/1758893871.run(Unknown Source)
*/
                    /*
                    r0 = this;
                    r0.twitterUrl = r1;
                    return;
                    */
                    throw new UnsupportedOperationException("Method not decompiled: net.gogame.gowrap.model.configuration.Configuration.Integrations.Core.LocaleConfiguration.setTwitterUrl(java.lang.String):void");
                }

                public java.lang.String getInstagramUrl() {
                    /* JADX: method processing error */
/*
Error: jadx.core.utils.exceptions.JadxRuntimeException: SSA rename variables already executed
	at jadx.core.dex.visitors.ssa.SSATransform.renameVariables(SSATransform.java:120)
	at jadx.core.dex.visitors.ssa.SSATransform.process(SSATransform.java:52)
	at jadx.core.dex.visitors.ssa.SSATransform.visit(SSATransform.java:42)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:31)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:17)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
	at jadx.core.ProcessClass.process(ProcessClass.java:34)
	at jadx.core.ProcessClass.processDependencies(ProcessClass.java:56)
	at jadx.core.ProcessClass.process(ProcessClass.java:39)
	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:282)
	at jadx.api.JavaClass.decompile(JavaClass.java:62)
	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:200)
	at jadx.api.JadxDecompiler$$Lambda$8/1758893871.run(Unknown Source)
*/
                    /*
                    r1 = this;
                    r0 = r1.instagramUrl;
                    return r0;
                    */
                    throw new UnsupportedOperationException("Method not decompiled: net.gogame.gowrap.model.configuration.Configuration.Integrations.Core.LocaleConfiguration.getInstagramUrl():java.lang.String");
                }

                public void setInstagramUrl(java.lang.String r1) {
                    /* JADX: method processing error */
/*
Error: jadx.core.utils.exceptions.JadxRuntimeException: SSA rename variables already executed
	at jadx.core.dex.visitors.ssa.SSATransform.renameVariables(SSATransform.java:120)
	at jadx.core.dex.visitors.ssa.SSATransform.process(SSATransform.java:52)
	at jadx.core.dex.visitors.ssa.SSATransform.visit(SSATransform.java:42)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:31)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:17)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
	at jadx.core.ProcessClass.process(ProcessClass.java:34)
	at jadx.core.ProcessClass.processDependencies(ProcessClass.java:56)
	at jadx.core.ProcessClass.process(ProcessClass.java:39)
	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:282)
	at jadx.api.JavaClass.decompile(JavaClass.java:62)
	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:200)
	at jadx.api.JadxDecompiler$$Lambda$8/1758893871.run(Unknown Source)
*/
                    /*
                    r0 = this;
                    r0.instagramUrl = r1;
                    return;
                    */
                    throw new UnsupportedOperationException("Method not decompiled: net.gogame.gowrap.model.configuration.Configuration.Integrations.Core.LocaleConfiguration.setInstagramUrl(java.lang.String):void");
                }

                public java.lang.String getYoutubeUrl() {
                    /* JADX: method processing error */
/*
Error: jadx.core.utils.exceptions.JadxRuntimeException: SSA rename variables already executed
	at jadx.core.dex.visitors.ssa.SSATransform.renameVariables(SSATransform.java:120)
	at jadx.core.dex.visitors.ssa.SSATransform.process(SSATransform.java:52)
	at jadx.core.dex.visitors.ssa.SSATransform.visit(SSATransform.java:42)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:31)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:17)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
	at jadx.core.ProcessClass.process(ProcessClass.java:34)
	at jadx.core.ProcessClass.processDependencies(ProcessClass.java:56)
	at jadx.core.ProcessClass.process(ProcessClass.java:39)
	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:282)
	at jadx.api.JavaClass.decompile(JavaClass.java:62)
	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:200)
	at jadx.api.JadxDecompiler$$Lambda$8/1758893871.run(Unknown Source)
*/
                    /*
                    r1 = this;
                    r0 = r1.youtubeUrl;
                    return r0;
                    */
                    throw new UnsupportedOperationException("Method not decompiled: net.gogame.gowrap.model.configuration.Configuration.Integrations.Core.LocaleConfiguration.getYoutubeUrl():java.lang.String");
                }

                public void setYoutubeUrl(java.lang.String r1) {
                    /* JADX: method processing error */
/*
Error: jadx.core.utils.exceptions.JadxRuntimeException: SSA rename variables already executed
	at jadx.core.dex.visitors.ssa.SSATransform.renameVariables(SSATransform.java:120)
	at jadx.core.dex.visitors.ssa.SSATransform.process(SSATransform.java:52)
	at jadx.core.dex.visitors.ssa.SSATransform.visit(SSATransform.java:42)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:31)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:17)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
	at jadx.core.ProcessClass.process(ProcessClass.java:34)
	at jadx.core.ProcessClass.processDependencies(ProcessClass.java:56)
	at jadx.core.ProcessClass.process(ProcessClass.java:39)
	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:282)
	at jadx.api.JavaClass.decompile(JavaClass.java:62)
	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:200)
	at jadx.api.JadxDecompiler$$Lambda$8/1758893871.run(Unknown Source)
*/
                    /*
                    r0 = this;
                    r0.youtubeUrl = r1;
                    return;
                    */
                    throw new UnsupportedOperationException("Method not decompiled: net.gogame.gowrap.model.configuration.Configuration.Integrations.Core.LocaleConfiguration.setYoutubeUrl(java.lang.String):void");
                }

                public java.lang.String getForumUrl() {
                    /* JADX: method processing error */
/*
Error: jadx.core.utils.exceptions.JadxRuntimeException: SSA rename variables already executed
	at jadx.core.dex.visitors.ssa.SSATransform.renameVariables(SSATransform.java:120)
	at jadx.core.dex.visitors.ssa.SSATransform.process(SSATransform.java:52)
	at jadx.core.dex.visitors.ssa.SSATransform.visit(SSATransform.java:42)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:31)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:17)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
	at jadx.core.ProcessClass.process(ProcessClass.java:34)
	at jadx.core.ProcessClass.processDependencies(ProcessClass.java:56)
	at jadx.core.ProcessClass.process(ProcessClass.java:39)
	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:282)
	at jadx.api.JavaClass.decompile(JavaClass.java:62)
	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:200)
	at jadx.api.JadxDecompiler$$Lambda$8/1758893871.run(Unknown Source)
*/
                    /*
                    r1 = this;
                    r0 = r1.forumUrl;
                    return r0;
                    */
                    throw new UnsupportedOperationException("Method not decompiled: net.gogame.gowrap.model.configuration.Configuration.Integrations.Core.LocaleConfiguration.getForumUrl():java.lang.String");
                }

                public void setForumUrl(java.lang.String r1) {
                    /* JADX: method processing error */
/*
Error: jadx.core.utils.exceptions.JadxRuntimeException: SSA rename variables already executed
	at jadx.core.dex.visitors.ssa.SSATransform.renameVariables(SSATransform.java:120)
	at jadx.core.dex.visitors.ssa.SSATransform.process(SSATransform.java:52)
	at jadx.core.dex.visitors.ssa.SSATransform.visit(SSATransform.java:42)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:31)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:17)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
	at jadx.core.ProcessClass.process(ProcessClass.java:34)
	at jadx.core.ProcessClass.processDependencies(ProcessClass.java:56)
	at jadx.core.ProcessClass.process(ProcessClass.java:39)
	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:282)
	at jadx.api.JavaClass.decompile(JavaClass.java:62)
	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:200)
	at jadx.api.JadxDecompiler$$Lambda$8/1758893871.run(Unknown Source)
*/
                    /*
                    r0 = this;
                    r0.forumUrl = r1;
                    return;
                    */
                    throw new UnsupportedOperationException("Method not decompiled: net.gogame.gowrap.model.configuration.Configuration.Integrations.Core.LocaleConfiguration.setForumUrl(java.lang.String):void");
                }

                public java.lang.String getWikiUrl() {
                    /* JADX: method processing error */
/*
Error: jadx.core.utils.exceptions.JadxRuntimeException: SSA rename variables already executed
	at jadx.core.dex.visitors.ssa.SSATransform.renameVariables(SSATransform.java:120)
	at jadx.core.dex.visitors.ssa.SSATransform.process(SSATransform.java:52)
	at jadx.core.dex.visitors.ssa.SSATransform.visit(SSATransform.java:42)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:31)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:17)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
	at jadx.core.ProcessClass.process(ProcessClass.java:34)
	at jadx.core.ProcessClass.processDependencies(ProcessClass.java:56)
	at jadx.core.ProcessClass.process(ProcessClass.java:39)
	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:282)
	at jadx.api.JavaClass.decompile(JavaClass.java:62)
	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:200)
	at jadx.api.JadxDecompiler$$Lambda$8/1758893871.run(Unknown Source)
*/
                    /*
                    r1 = this;
                    r0 = r1.wikiUrl;
                    return r0;
                    */
                    throw new UnsupportedOperationException("Method not decompiled: net.gogame.gowrap.model.configuration.Configuration.Integrations.Core.LocaleConfiguration.getWikiUrl():java.lang.String");
                }

                public void setWikiUrl(java.lang.String r1) {
                    /* JADX: method processing error */
/*
Error: jadx.core.utils.exceptions.JadxRuntimeException: SSA rename variables already executed
	at jadx.core.dex.visitors.ssa.SSATransform.renameVariables(SSATransform.java:120)
	at jadx.core.dex.visitors.ssa.SSATransform.process(SSATransform.java:52)
	at jadx.core.dex.visitors.ssa.SSATransform.visit(SSATransform.java:42)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:31)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:17)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
	at jadx.core.ProcessClass.process(ProcessClass.java:34)
	at jadx.core.ProcessClass.processDependencies(ProcessClass.java:56)
	at jadx.core.ProcessClass.process(ProcessClass.java:39)
	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:282)
	at jadx.api.JavaClass.decompile(JavaClass.java:62)
	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:200)
	at jadx.api.JadxDecompiler$$Lambda$8/1758893871.run(Unknown Source)
*/
                    /*
                    r0 = this;
                    r0.wikiUrl = r1;
                    return;
                    */
                    throw new UnsupportedOperationException("Method not decompiled: net.gogame.gowrap.model.configuration.Configuration.Integrations.Core.LocaleConfiguration.setWikiUrl(java.lang.String):void");
                }
            }

            public Core(org.json.JSONObject r7) throws org.json.JSONException {
                /* JADX: method processing error */
/*
Error: jadx.core.utils.exceptions.JadxRuntimeException: SSA rename variables already executed
	at jadx.core.dex.visitors.ssa.SSATransform.renameVariables(SSATransform.java:120)
	at jadx.core.dex.visitors.ssa.SSATransform.process(SSATransform.java:52)
	at jadx.core.dex.visitors.ssa.SSATransform.visit(SSATransform.java:42)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:31)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:17)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
	at jadx.core.ProcessClass.process(ProcessClass.java:34)
	at jadx.core.ProcessClass.processDependencies(ProcessClass.java:56)
	at jadx.core.ProcessClass.process(ProcessClass.java:39)
	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:282)
	at jadx.api.JavaClass.decompile(JavaClass.java:62)
	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:200)
	at jadx.api.JadxDecompiler$$Lambda$8/1758893871.run(Unknown Source)
*/
                /*
                r6 = this;
                r6.<init>();
                r0 = "locales";
                r0 = r7.has(r0);
                if (r0 == 0) goto L_0x0042;
            L_0x000b:
                r0 = "locales";
                r1 = r7.optJSONObject(r0);
                if (r1 == 0) goto L_0x0042;
            L_0x0013:
                r0 = new java.util.HashMap;
                r0.<init>();
                r6.locales = r0;
                r2 = r1.keys();
            L_0x001e:
                r0 = r2.hasNext();
                if (r0 == 0) goto L_0x0042;
            L_0x0024:
                r0 = r2.next();
                r0 = (java.lang.String) r0;
                r3 = r1.optJSONObject(r0);
                if (r3 == 0) goto L_0x003b;
            L_0x0030:
                r4 = r6.locales;
                r5 = new net.gogame.gowrap.model.configuration.Configuration$Integrations$Core$LocaleConfiguration;
                r5.<init>(r3);
                r4.put(r0, r5);
                goto L_0x001e;
            L_0x003b:
                r3 = r6.locales;
                r4 = 0;
                r3.put(r0, r4);
                goto L_0x001e;
            L_0x0042:
                r0 = "supportedLocales";
                r0 = r7.has(r0);
                if (r0 == 0) goto L_0x006c;
            L_0x004a:
                r0 = "supportedLocales";
                r1 = r7.optJSONArray(r0);
                if (r1 == 0) goto L_0x006c;
            L_0x0052:
                r0 = new java.util.ArrayList;
                r0.<init>();
                r6.supportedLocales = r0;
                r0 = 0;
            L_0x005a:
                r2 = r1.length();
                if (r0 >= r2) goto L_0x006c;
            L_0x0060:
                r2 = r6.supportedLocales;
                r3 = r1.getString(r0);
                r2.add(r3);
                r0 = r0 + 1;
                goto L_0x005a;
            L_0x006c:
                r0 = "slideOut";
                r0 = net.gogame.gowrap.support.JSONUtils.optBoolean(r7, r0);
                r6.slideOut = r0;
                r0 = "slideIn";
                r0 = net.gogame.gowrap.support.JSONUtils.optBoolean(r7, r0);
                r6.slideIn = r0;
                return;
                */
                throw new UnsupportedOperationException("Method not decompiled: net.gogame.gowrap.model.configuration.Configuration.Integrations.Core.<init>(org.json.JSONObject):void");
            }

            public java.util.Map<java.lang.String, net.gogame.gowrap.model.configuration.Configuration.Integrations.Core.LocaleConfiguration> getLocales() {
                /* JADX: method processing error */
/*
Error: jadx.core.utils.exceptions.JadxRuntimeException: SSA rename variables already executed
	at jadx.core.dex.visitors.ssa.SSATransform.renameVariables(SSATransform.java:120)
	at jadx.core.dex.visitors.ssa.SSATransform.process(SSATransform.java:52)
	at jadx.core.dex.visitors.ssa.SSATransform.visit(SSATransform.java:42)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:31)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:17)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
	at jadx.core.ProcessClass.process(ProcessClass.java:34)
	at jadx.core.ProcessClass.processDependencies(ProcessClass.java:56)
	at jadx.core.ProcessClass.process(ProcessClass.java:39)
	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:282)
	at jadx.api.JavaClass.decompile(JavaClass.java:62)
	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:200)
	at jadx.api.JadxDecompiler$$Lambda$8/1758893871.run(Unknown Source)
*/
                /*
                r1 = this;
                r0 = r1.locales;
                return r0;
                */
                throw new UnsupportedOperationException("Method not decompiled: net.gogame.gowrap.model.configuration.Configuration.Integrations.Core.getLocales():java.util.Map<java.lang.String, net.gogame.gowrap.model.configuration.Configuration$Integrations$Core$LocaleConfiguration>");
            }

            public void setLocales(java.util.Map<java.lang.String, net.gogame.gowrap.model.configuration.Configuration.Integrations.Core.LocaleConfiguration> r1) {
                /* JADX: method processing error */
/*
Error: jadx.core.utils.exceptions.JadxRuntimeException: SSA rename variables already executed
	at jadx.core.dex.visitors.ssa.SSATransform.renameVariables(SSATransform.java:120)
	at jadx.core.dex.visitors.ssa.SSATransform.process(SSATransform.java:52)
	at jadx.core.dex.visitors.ssa.SSATransform.visit(SSATransform.java:42)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:31)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:17)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
	at jadx.core.ProcessClass.process(ProcessClass.java:34)
	at jadx.core.ProcessClass.processDependencies(ProcessClass.java:56)
	at jadx.core.ProcessClass.process(ProcessClass.java:39)
	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:282)
	at jadx.api.JavaClass.decompile(JavaClass.java:62)
	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:200)
	at jadx.api.JadxDecompiler$$Lambda$8/1758893871.run(Unknown Source)
*/
                /*
                r0 = this;
                r0.locales = r1;
                return;
                */
                throw new UnsupportedOperationException("Method not decompiled: net.gogame.gowrap.model.configuration.Configuration.Integrations.Core.setLocales(java.util.Map):void");
            }

            public java.util.List<java.lang.String> getSupportedLocales() {
                /* JADX: method processing error */
/*
Error: jadx.core.utils.exceptions.JadxRuntimeException: SSA rename variables already executed
	at jadx.core.dex.visitors.ssa.SSATransform.renameVariables(SSATransform.java:120)
	at jadx.core.dex.visitors.ssa.SSATransform.process(SSATransform.java:52)
	at jadx.core.dex.visitors.ssa.SSATransform.visit(SSATransform.java:42)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:31)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:17)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
	at jadx.core.ProcessClass.process(ProcessClass.java:34)
	at jadx.core.ProcessClass.processDependencies(ProcessClass.java:56)
	at jadx.core.ProcessClass.process(ProcessClass.java:39)
	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:282)
	at jadx.api.JavaClass.decompile(JavaClass.java:62)
	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:200)
	at jadx.api.JadxDecompiler$$Lambda$8/1758893871.run(Unknown Source)
*/
                /*
                r1 = this;
                r0 = r1.supportedLocales;
                return r0;
                */
                throw new UnsupportedOperationException("Method not decompiled: net.gogame.gowrap.model.configuration.Configuration.Integrations.Core.getSupportedLocales():java.util.List<java.lang.String>");
            }

            public void setSupportedLocales(java.util.List<java.lang.String> r1) {
                /* JADX: method processing error */
/*
Error: jadx.core.utils.exceptions.JadxRuntimeException: SSA rename variables already executed
	at jadx.core.dex.visitors.ssa.SSATransform.renameVariables(SSATransform.java:120)
	at jadx.core.dex.visitors.ssa.SSATransform.process(SSATransform.java:52)
	at jadx.core.dex.visitors.ssa.SSATransform.visit(SSATransform.java:42)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:31)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:17)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
	at jadx.core.ProcessClass.process(ProcessClass.java:34)
	at jadx.core.ProcessClass.processDependencies(ProcessClass.java:56)
	at jadx.core.ProcessClass.process(ProcessClass.java:39)
	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:282)
	at jadx.api.JavaClass.decompile(JavaClass.java:62)
	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:200)
	at jadx.api.JadxDecompiler$$Lambda$8/1758893871.run(Unknown Source)
*/
                /*
                r0 = this;
                r0.supportedLocales = r1;
                return;
                */
                throw new UnsupportedOperationException("Method not decompiled: net.gogame.gowrap.model.configuration.Configuration.Integrations.Core.setSupportedLocales(java.util.List):void");
            }

            public java.lang.Boolean getSlideOut() {
                /* JADX: method processing error */
/*
Error: jadx.core.utils.exceptions.JadxRuntimeException: SSA rename variables already executed
	at jadx.core.dex.visitors.ssa.SSATransform.renameVariables(SSATransform.java:120)
	at jadx.core.dex.visitors.ssa.SSATransform.process(SSATransform.java:52)
	at jadx.core.dex.visitors.ssa.SSATransform.visit(SSATransform.java:42)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:31)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:17)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
	at jadx.core.ProcessClass.process(ProcessClass.java:34)
	at jadx.core.ProcessClass.processDependencies(ProcessClass.java:56)
	at jadx.core.ProcessClass.process(ProcessClass.java:39)
	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:282)
	at jadx.api.JavaClass.decompile(JavaClass.java:62)
	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:200)
	at jadx.api.JadxDecompiler$$Lambda$8/1758893871.run(Unknown Source)
*/
                /*
                r1 = this;
                r0 = r1.slideOut;
                return r0;
                */
                throw new UnsupportedOperationException("Method not decompiled: net.gogame.gowrap.model.configuration.Configuration.Integrations.Core.getSlideOut():java.lang.Boolean");
            }

            public void setSlideOut(java.lang.Boolean r1) {
                /* JADX: method processing error */
/*
Error: jadx.core.utils.exceptions.JadxRuntimeException: SSA rename variables already executed
	at jadx.core.dex.visitors.ssa.SSATransform.renameVariables(SSATransform.java:120)
	at jadx.core.dex.visitors.ssa.SSATransform.process(SSATransform.java:52)
	at jadx.core.dex.visitors.ssa.SSATransform.visit(SSATransform.java:42)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:31)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:17)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
	at jadx.core.ProcessClass.process(ProcessClass.java:34)
	at jadx.core.ProcessClass.processDependencies(ProcessClass.java:56)
	at jadx.core.ProcessClass.process(ProcessClass.java:39)
	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:282)
	at jadx.api.JavaClass.decompile(JavaClass.java:62)
	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:200)
	at jadx.api.JadxDecompiler$$Lambda$8/1758893871.run(Unknown Source)
*/
                /*
                r0 = this;
                r0.slideOut = r1;
                return;
                */
                throw new UnsupportedOperationException("Method not decompiled: net.gogame.gowrap.model.configuration.Configuration.Integrations.Core.setSlideOut(java.lang.Boolean):void");
            }

            public java.lang.Boolean getSlideIn() {
                /* JADX: method processing error */
/*
Error: jadx.core.utils.exceptions.JadxRuntimeException: SSA rename variables already executed
	at jadx.core.dex.visitors.ssa.SSATransform.renameVariables(SSATransform.java:120)
	at jadx.core.dex.visitors.ssa.SSATransform.process(SSATransform.java:52)
	at jadx.core.dex.visitors.ssa.SSATransform.visit(SSATransform.java:42)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:31)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:17)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
	at jadx.core.ProcessClass.process(ProcessClass.java:34)
	at jadx.core.ProcessClass.processDependencies(ProcessClass.java:56)
	at jadx.core.ProcessClass.process(ProcessClass.java:39)
	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:282)
	at jadx.api.JavaClass.decompile(JavaClass.java:62)
	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:200)
	at jadx.api.JadxDecompiler$$Lambda$8/1758893871.run(Unknown Source)
*/
                /*
                r1 = this;
                r0 = r1.slideIn;
                return r0;
                */
                throw new UnsupportedOperationException("Method not decompiled: net.gogame.gowrap.model.configuration.Configuration.Integrations.Core.getSlideIn():java.lang.Boolean");
            }

            public void setSlideIn(java.lang.Boolean r1) {
                /* JADX: method processing error */
/*
Error: jadx.core.utils.exceptions.JadxRuntimeException: SSA rename variables already executed
	at jadx.core.dex.visitors.ssa.SSATransform.renameVariables(SSATransform.java:120)
	at jadx.core.dex.visitors.ssa.SSATransform.process(SSATransform.java:52)
	at jadx.core.dex.visitors.ssa.SSATransform.visit(SSATransform.java:42)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:31)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:17)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
	at jadx.core.ProcessClass.process(ProcessClass.java:34)
	at jadx.core.ProcessClass.processDependencies(ProcessClass.java:56)
	at jadx.core.ProcessClass.process(ProcessClass.java:39)
	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:282)
	at jadx.api.JavaClass.decompile(JavaClass.java:62)
	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:200)
	at jadx.api.JadxDecompiler$$Lambda$8/1758893871.run(Unknown Source)
*/
                /*
                r0 = this;
                r0.slideIn = r1;
                return;
                */
                throw new UnsupportedOperationException("Method not decompiled: net.gogame.gowrap.model.configuration.Configuration.Integrations.Core.setSlideIn(java.lang.Boolean):void");
            }
        }

        public Integrations(org.json.JSONObject r3) throws org.json.JSONException {
            /* JADX: method processing error */
/*
Error: jadx.core.utils.exceptions.JadxRuntimeException: SSA rename variables already executed
	at jadx.core.dex.visitors.ssa.SSATransform.renameVariables(SSATransform.java:120)
	at jadx.core.dex.visitors.ssa.SSATransform.process(SSATransform.java:52)
	at jadx.core.dex.visitors.ssa.SSATransform.visit(SSATransform.java:42)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:31)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:17)
	at jadx.core.ProcessClass.process(ProcessClass.java:34)
	at jadx.core.ProcessClass.processDependencies(ProcessClass.java:56)
	at jadx.core.ProcessClass.process(ProcessClass.java:39)
	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:282)
	at jadx.api.JavaClass.decompile(JavaClass.java:62)
	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:200)
	at jadx.api.JadxDecompiler$$Lambda$8/1758893871.run(Unknown Source)
*/
            /*
            r2 = this;
            r2.<init>();
            r0 = "core";
            r0 = r3.has(r0);
            if (r0 == 0) goto L_0x001a;
        L_0x000b:
            r0 = "core";
            r0 = r3.optJSONObject(r0);
            if (r0 == 0) goto L_0x001a;
        L_0x0013:
            r1 = new net.gogame.gowrap.model.configuration.Configuration$Integrations$Core;
            r1.<init>(r0);
            r2.core = r1;
        L_0x001a:
            return;
            */
            throw new UnsupportedOperationException("Method not decompiled: net.gogame.gowrap.model.configuration.Configuration.Integrations.<init>(org.json.JSONObject):void");
        }

        public net.gogame.gowrap.model.configuration.Configuration.Integrations.Core getCore() {
            /* JADX: method processing error */
/*
Error: jadx.core.utils.exceptions.JadxRuntimeException: SSA rename variables already executed
	at jadx.core.dex.visitors.ssa.SSATransform.renameVariables(SSATransform.java:120)
	at jadx.core.dex.visitors.ssa.SSATransform.process(SSATransform.java:52)
	at jadx.core.dex.visitors.ssa.SSATransform.visit(SSATransform.java:42)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:31)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:17)
	at jadx.core.ProcessClass.process(ProcessClass.java:34)
	at jadx.core.ProcessClass.processDependencies(ProcessClass.java:56)
	at jadx.core.ProcessClass.process(ProcessClass.java:39)
	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:282)
	at jadx.api.JavaClass.decompile(JavaClass.java:62)
	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:200)
	at jadx.api.JadxDecompiler$$Lambda$8/1758893871.run(Unknown Source)
*/
            /*
            r1 = this;
            r0 = r1.core;
            return r0;
            */
            throw new UnsupportedOperationException("Method not decompiled: net.gogame.gowrap.model.configuration.Configuration.Integrations.getCore():net.gogame.gowrap.model.configuration.Configuration$Integrations$Core");
        }

        public void setCore(net.gogame.gowrap.model.configuration.Configuration.Integrations.Core r1) {
            /* JADX: method processing error */
/*
Error: jadx.core.utils.exceptions.JadxRuntimeException: SSA rename variables already executed
	at jadx.core.dex.visitors.ssa.SSATransform.renameVariables(SSATransform.java:120)
	at jadx.core.dex.visitors.ssa.SSATransform.process(SSATransform.java:52)
	at jadx.core.dex.visitors.ssa.SSATransform.visit(SSATransform.java:42)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:31)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:17)
	at jadx.core.ProcessClass.process(ProcessClass.java:34)
	at jadx.core.ProcessClass.processDependencies(ProcessClass.java:56)
	at jadx.core.ProcessClass.process(ProcessClass.java:39)
	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:282)
	at jadx.api.JavaClass.decompile(JavaClass.java:62)
	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:200)
	at jadx.api.JadxDecompiler$$Lambda$8/1758893871.run(Unknown Source)
*/
            /*
            r0 = this;
            r0.core = r1;
            return;
            */
            throw new UnsupportedOperationException("Method not decompiled: net.gogame.gowrap.model.configuration.Configuration.Integrations.setCore(net.gogame.gowrap.model.configuration.Configuration$Integrations$Core):void");
        }
    }

    public static class Settings {
        private Boolean chatBotEnabled;
        private String newsWidgetVersion;

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

    public Configuration(JSONObject jSONObject) throws JSONException {
        if (jSONObject != null) {
            JSONObject optJSONObject;
            if (jSONObject.has("integrations")) {
                optJSONObject = jSONObject.optJSONObject("integrations");
                if (optJSONObject != null) {
                    this.integrations = new Integrations(optJSONObject);
                }
            }
            if (jSONObject.has("settings")) {
                optJSONObject = jSONObject.optJSONObject("settings");
                if (optJSONObject != null) {
                    this.settings = new Settings(optJSONObject);
                }
            }
        }
    }

    public Integrations getIntegrations() {
        return this.integrations;
    }

    public void setIntegrations(Integrations integrations) {
        this.integrations = integrations;
    }

    public Settings getSettings() {
        return this.settings;
    }

    public void setSettings(Settings settings) {
        this.settings = settings;
    }
}
