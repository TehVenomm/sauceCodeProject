.class public Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;
.super Ljava/lang/Object;
.source "Configuration.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "LocaleConfiguration"
.end annotation


# instance fields
.field private facebookUrl:Ljava/lang/String;

.field private forumUrl:Ljava/lang/String;

.field private instagramUrl:Ljava/lang/String;

.field private twitterUrl:Ljava/lang/String;

.field private whatsNewUrl:Ljava/lang/String;

.field private wikiUrl:Ljava/lang/String;

.field private youtubeUrl:Ljava/lang/String;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 173
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public constructor <init>(Lorg/json/JSONObject;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    .line 177
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const-string v0, "whatsNewUrl"

    .line 179
    invoke-static {p1, v0}, Lnet/gogame/gowrap/support/JSONUtils;->optUrl(Lorg/json/JSONObject;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p0, v0}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;->setWhatsNewUrl(Ljava/lang/String;)V

    const-string v0, "facebookUrl"

    .line 180
    invoke-static {p1, v0}, Lnet/gogame/gowrap/support/JSONUtils;->optUrl(Lorg/json/JSONObject;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p0, v0}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;->setFacebookUrl(Ljava/lang/String;)V

    const-string v0, "twitterUrl"

    .line 181
    invoke-static {p1, v0}, Lnet/gogame/gowrap/support/JSONUtils;->optUrl(Lorg/json/JSONObject;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p0, v0}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;->setTwitterUrl(Ljava/lang/String;)V

    const-string v0, "instagramUrl"

    .line 182
    invoke-static {p1, v0}, Lnet/gogame/gowrap/support/JSONUtils;->optUrl(Lorg/json/JSONObject;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p0, v0}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;->setInstagramUrl(Ljava/lang/String;)V

    const-string v0, "youtubeUrl"

    .line 183
    invoke-static {p1, v0}, Lnet/gogame/gowrap/support/JSONUtils;->optUrl(Lorg/json/JSONObject;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p0, v0}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;->setYoutubeUrl(Ljava/lang/String;)V

    const-string v0, "forumUrl"

    .line 184
    invoke-static {p1, v0}, Lnet/gogame/gowrap/support/JSONUtils;->optUrl(Lorg/json/JSONObject;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p0, v0}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;->setForumUrl(Ljava/lang/String;)V

    const-string v0, "wikiUrl"

    .line 185
    invoke-static {p1, v0}, Lnet/gogame/gowrap/support/JSONUtils;->optUrl(Lorg/json/JSONObject;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;->setWikiUrl(Ljava/lang/String;)V

    return-void
.end method


# virtual methods
.method public getFacebookUrl()Ljava/lang/String;
    .locals 1

    .line 197
    iget-object v0, p0, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;->facebookUrl:Ljava/lang/String;

    return-object v0
.end method

.method public getForumUrl()Ljava/lang/String;
    .locals 1

    .line 229
    iget-object v0, p0, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;->forumUrl:Ljava/lang/String;

    return-object v0
.end method

.method public getInstagramUrl()Ljava/lang/String;
    .locals 1

    .line 213
    iget-object v0, p0, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;->instagramUrl:Ljava/lang/String;

    return-object v0
.end method

.method public getTwitterUrl()Ljava/lang/String;
    .locals 1

    .line 205
    iget-object v0, p0, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;->twitterUrl:Ljava/lang/String;

    return-object v0
.end method

.method public getWhatsNewUrl()Ljava/lang/String;
    .locals 1

    .line 189
    iget-object v0, p0, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;->whatsNewUrl:Ljava/lang/String;

    return-object v0
.end method

.method public getWikiUrl()Ljava/lang/String;
    .locals 1

    .line 237
    iget-object v0, p0, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;->wikiUrl:Ljava/lang/String;

    return-object v0
.end method

.method public getYoutubeUrl()Ljava/lang/String;
    .locals 1

    .line 221
    iget-object v0, p0, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;->youtubeUrl:Ljava/lang/String;

    return-object v0
.end method

.method public setFacebookUrl(Ljava/lang/String;)V
    .locals 0

    .line 201
    iput-object p1, p0, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;->facebookUrl:Ljava/lang/String;

    return-void
.end method

.method public setForumUrl(Ljava/lang/String;)V
    .locals 0

    .line 233
    iput-object p1, p0, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;->forumUrl:Ljava/lang/String;

    return-void
.end method

.method public setInstagramUrl(Ljava/lang/String;)V
    .locals 0

    .line 217
    iput-object p1, p0, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;->instagramUrl:Ljava/lang/String;

    return-void
.end method

.method public setTwitterUrl(Ljava/lang/String;)V
    .locals 0

    .line 209
    iput-object p1, p0, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;->twitterUrl:Ljava/lang/String;

    return-void
.end method

.method public setWhatsNewUrl(Ljava/lang/String;)V
    .locals 0

    .line 193
    iput-object p1, p0, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;->whatsNewUrl:Ljava/lang/String;

    return-void
.end method

.method public setWikiUrl(Ljava/lang/String;)V
    .locals 0

    .line 241
    iput-object p1, p0, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;->wikiUrl:Ljava/lang/String;

    return-void
.end method

.method public setYoutubeUrl(Ljava/lang/String;)V
    .locals 0

    .line 225
    iput-object p1, p0, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;->youtubeUrl:Ljava/lang/String;

    return-void
.end method
