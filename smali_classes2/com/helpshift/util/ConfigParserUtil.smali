.class public Lcom/helpshift/util/ConfigParserUtil;
.super Ljava/lang/Object;
.source "ConfigParserUtil.java"


# static fields
.field private static final TAG:Ljava/lang/String; = "ConfigParserUtil"


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    .line 21
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method private static fixMetaData(Ljava/util/HashMap;)V
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)V"
        }
    .end annotation

    if-nez p0, :cond_0

    return-void

    .line 224
    :cond_0
    invoke-virtual {p0}, Ljava/util/HashMap;->keySet()Ljava/util/Set;

    move-result-object v0

    .line 225
    invoke-interface {v0}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_1
    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_4

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/String;

    .line 226
    invoke-virtual {p0, v1}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v2

    .line 227
    instance-of v3, v2, Ljava/lang/String;

    if-eqz v3, :cond_1

    .line 228
    invoke-virtual {v2}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v2

    const-string v3, "yes"

    .line 229
    invoke-virtual {v2, v3}, Ljava/lang/String;->equalsIgnoreCase(Ljava/lang/String;)Z

    move-result v3

    if-nez v3, :cond_2

    const-string v3, "no"

    invoke-virtual {v2, v3}, Ljava/lang/String;->equalsIgnoreCase(Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_1

    :cond_2
    const-string v3, "yes"

    .line 230
    invoke-virtual {v2, v3}, Ljava/lang/String;->equalsIgnoreCase(Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_3

    const-string v2, "true"

    goto :goto_1

    :cond_3
    const-string v2, "false"

    .line 231
    :goto_1
    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Ljava/lang/String;)Ljava/lang/Boolean;

    move-result-object v2

    invoke-virtual {p0, v1, v2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    :cond_4
    const-string v0, "hs-tags"

    .line 236
    invoke-virtual {p0, v0}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/util/ArrayList;

    if-eqz v0, :cond_5

    .line 237
    invoke-virtual {v0}, Ljava/util/ArrayList;->size()I

    move-result v1

    if-lez v1, :cond_5

    const/4 v1, 0x0

    .line 238
    new-array v1, v1, [Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/util/ArrayList;->toArray([Ljava/lang/Object;)[Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Ljava/lang/String;

    const-string v1, "hs-tags"

    .line 239
    invoke-virtual {p0, v1, v0}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_5
    return-void
.end method

.method public static parseConfigDictionary(Landroid/content/Context;Ljava/lang/String;)Ljava/util/Map;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Ljava/lang/String;",
            ")",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    .line 26
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0, p1}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    .line 27
    invoke-static {v0}, Lcom/helpshift/util/HSJSONUtils;->toMap(Lorg/json/JSONObject;)Ljava/util/HashMap;

    move-result-object p1

    .line 28
    invoke-static {p0, p1}, Lcom/helpshift/util/ConfigParserUtil;->parseConfigDictionary(Landroid/content/Context;Ljava/util/Map;)Ljava/util/Map;

    move-result-object p0

    return-object p0
.end method

.method public static parseConfigDictionary(Landroid/content/Context;Ljava/util/Map;)Ljava/util/Map;
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation

    if-nez p1, :cond_0

    .line 47
    new-instance p0, Ljava/util/HashMap;

    invoke-direct {p0}, Ljava/util/HashMap;-><init>()V

    return-object p0

    :cond_0
    const-string v0, "enableContactUs"

    .line 50
    invoke-interface {p1, v0}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    if-eqz v0, :cond_6

    const-string v1, "yes"

    .line 52
    invoke-virtual {v0, v1}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-nez v1, :cond_5

    const-string v1, "always"

    invoke-virtual {v0, v1}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    goto :goto_1

    :cond_1
    const-string v1, "no"

    .line 55
    invoke-virtual {v0, v1}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-nez v1, :cond_4

    const-string v1, "never"

    invoke-virtual {v0, v1}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_2

    goto :goto_0

    :cond_2
    const-string v1, "after_viewing_faqs"

    .line 58
    invoke-virtual {v0, v1}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_3

    const-string v0, "enableContactUs"

    .line 59
    sget-object v1, Lcom/helpshift/support/Support$EnableContactUs;->AFTER_VIEWING_FAQS:Ljava/lang/Integer;

    invoke-interface {p1, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_2

    :cond_3
    const-string v1, "after_marking_answer_unhelpful"

    .line 61
    invoke-virtual {v0, v1}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_6

    const-string v0, "enableContactUs"

    .line 62
    sget-object v1, Lcom/helpshift/support/Support$EnableContactUs;->AFTER_MARKING_ANSWER_UNHELPFUL:Ljava/lang/Integer;

    invoke-interface {p1, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_2

    :cond_4
    :goto_0
    const-string v0, "enableContactUs"

    .line 56
    sget-object v1, Lcom/helpshift/support/Support$EnableContactUs;->NEVER:Ljava/lang/Integer;

    invoke-interface {p1, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_2

    :cond_5
    :goto_1
    const-string v0, "enableContactUs"

    .line 53
    sget-object v1, Lcom/helpshift/support/Support$EnableContactUs;->ALWAYS:Ljava/lang/Integer;

    invoke-interface {p1, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_6
    :goto_2
    const-string v0, "notificationIcon"

    .line 66
    invoke-interface {p1, v0}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    .line 67
    instance-of v1, v0, Ljava/lang/String;

    if-eqz v1, :cond_8

    .line 68
    check-cast v0, Ljava/lang/String;

    .line 69
    invoke-virtual {p0}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    const-string v2, "drawable"

    .line 71
    invoke-virtual {p0}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v3

    .line 69
    invoke-virtual {v1, v0, v2, v3}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v0

    if-eqz v0, :cond_7

    const-string v1, "notificationIcon"

    .line 73
    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    invoke-interface {p1, v1, v0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_3

    :cond_7
    const-string v0, "notificationIcon"

    .line 76
    invoke-interface {p1, v0}, Ljava/util/Map;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    :cond_8
    :goto_3
    const-string v0, "largeNotificationIcon"

    .line 80
    invoke-interface {p1, v0}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    .line 81
    instance-of v1, v0, Ljava/lang/String;

    if-eqz v1, :cond_a

    .line 82
    check-cast v0, Ljava/lang/String;

    .line 83
    invoke-virtual {p0}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    const-string v2, "drawable"

    .line 85
    invoke-virtual {p0}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v3

    .line 83
    invoke-virtual {v1, v0, v2, v3}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v0

    if-eqz v0, :cond_9

    const-string v1, "largeNotificationIcon"

    .line 87
    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    invoke-interface {p1, v1, v0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_4

    :cond_9
    const-string v0, "largeNotificationIcon"

    .line 90
    invoke-interface {p1, v0}, Ljava/util/Map;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    :cond_a
    :goto_4
    const-string v0, "notificationSound"

    .line 94
    invoke-interface {p1, v0}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    .line 95
    instance-of v1, v0, Ljava/lang/String;

    if-eqz v1, :cond_c

    .line 96
    check-cast v0, Ljava/lang/String;

    .line 97
    invoke-virtual {p0}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    const-string v2, "raw"

    .line 99
    invoke-virtual {p0}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v3

    .line 97
    invoke-virtual {v1, v0, v2, v3}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v0

    if-eqz v0, :cond_b

    const-string v1, "notificationSound"

    .line 101
    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    invoke-interface {p1, v1, v0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_5

    :cond_b
    const-string v0, "notificationSound"

    .line 104
    invoke-interface {p1, v0}, Ljava/util/Map;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    .line 109
    :cond_c
    :goto_5
    new-instance v0, Ljava/util/HashSet;

    invoke-direct {v0}, Ljava/util/HashSet;-><init>()V

    const-string v1, "enableInAppNotification"

    .line 110
    invoke-virtual {v0, v1}, Ljava/util/HashSet;->add(Ljava/lang/Object;)Z

    const-string v1, "requireEmail"

    .line 111
    invoke-virtual {v0, v1}, Ljava/util/HashSet;->add(Ljava/lang/Object;)Z

    const-string v1, "hideNameAndEmail"

    .line 112
    invoke-virtual {v0, v1}, Ljava/util/HashSet;->add(Ljava/lang/Object;)Z

    const-string v1, "enableFullPrivacy"

    .line 113
    invoke-virtual {v0, v1}, Ljava/util/HashSet;->add(Ljava/lang/Object;)Z

    const-string v1, "showSearchOnNewConversation"

    .line 114
    invoke-virtual {v0, v1}, Ljava/util/HashSet;->add(Ljava/lang/Object;)Z

    const-string v1, "gotoConversationAfterContactUs"

    .line 115
    invoke-virtual {v0, v1}, Ljava/util/HashSet;->add(Ljava/lang/Object;)Z

    const-string v1, "showConversationResolutionQuestion"

    .line 116
    invoke-virtual {v0, v1}, Ljava/util/HashSet;->add(Ljava/lang/Object;)Z

    const-string v1, "enableDefaultFallbackLanguage"

    .line 117
    invoke-virtual {v0, v1}, Ljava/util/HashSet;->add(Ljava/lang/Object;)Z

    const-string v1, "enableChat"

    .line 118
    invoke-virtual {v0, v1}, Ljava/util/HashSet;->add(Ljava/lang/Object;)Z

    const-string v1, "enableInboxPolling"

    .line 119
    invoke-virtual {v0, v1}, Ljava/util/HashSet;->add(Ljava/lang/Object;)Z

    const-string v1, "enableLogging"

    .line 120
    invoke-virtual {v0, v1}, Ljava/util/HashSet;->add(Ljava/lang/Object;)Z

    const-string v1, "disableErrorReporting"

    .line 121
    invoke-virtual {v0, v1}, Ljava/util/HashSet;->add(Ljava/lang/Object;)Z

    const-string v1, "disableHelpshiftBranding"

    .line 122
    invoke-virtual {v0, v1}, Ljava/util/HashSet;->add(Ljava/lang/Object;)Z

    const-string v1, "showConversationInfoScreen"

    .line 123
    invoke-virtual {v0, v1}, Ljava/util/HashSet;->add(Ljava/lang/Object;)Z

    const-string v1, "disableAppLaunchEvent"

    .line 124
    invoke-virtual {v0, v1}, Ljava/util/HashSet;->add(Ljava/lang/Object;)Z

    const-string v1, "enableTypingIndicator"

    .line 125
    invoke-virtual {v0, v1}, Ljava/util/HashSet;->add(Ljava/lang/Object;)Z

    .line 126
    invoke-static {v0, p1}, Lcom/helpshift/util/ConfigParserUtil;->replaceWithBoolean(Ljava/util/HashSet;Ljava/util/Map;)Ljava/util/Map;

    move-result-object p1

    const-string v0, "hs-custom-metadata"

    .line 128
    invoke-interface {p1, v0}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/util/HashMap;

    .line 129
    invoke-static {v0}, Lcom/helpshift/util/ConfigParserUtil;->fixMetaData(Ljava/util/HashMap;)V

    if-eqz v0, :cond_d

    const-string v1, "hs-custom-metadata"

    .line 131
    invoke-interface {p1, v1, v0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 135
    :cond_d
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    const-string v1, "hs-custom-issue-field"

    .line 137
    invoke-interface {p1, v1}, Ljava/util/Map;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/util/HashMap;

    if-eqz v1, :cond_f

    .line 139
    invoke-virtual {v1}, Ljava/util/HashMap;->size()I

    move-result v2

    if-lez v2, :cond_f

    .line 140
    invoke-virtual {v1}, Ljava/util/HashMap;->entrySet()Ljava/util/Set;

    move-result-object v1

    invoke-interface {v1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :cond_e
    :goto_6
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_f

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/util/Map$Entry;

    .line 141
    invoke-interface {v2}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v3

    if-eqz v3, :cond_e

    .line 142
    invoke-interface {v2}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v3

    invoke-interface {v2}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Ljava/util/ArrayList;

    invoke-interface {v2}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/util/ArrayList;

    invoke-virtual {v2}, Ljava/util/ArrayList;->size()I

    move-result v2

    new-array v2, v2, [Ljava/lang/String;

    invoke-virtual {v4, v2}, Ljava/util/ArrayList;->toArray([Ljava/lang/Object;)[Ljava/lang/Object;

    move-result-object v2

    invoke-virtual {v0, v3, v2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_6

    .line 147
    :cond_f
    invoke-virtual {v0}, Ljava/util/HashMap;->size()I

    move-result v1

    if-lez v1, :cond_10

    const-string v1, "hs-custom-issue-field"

    .line 148
    invoke-interface {p1, v1, v0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_10
    const-string v0, "withTagsMatching"

    .line 151
    invoke-interface {p1, v0}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/util/HashMap;

    if-eqz v0, :cond_12

    const-string v1, "tags"

    .line 153
    invoke-virtual {v0, v1}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/util/ArrayList;

    if-eqz v1, :cond_11

    .line 154
    invoke-virtual {v1}, Ljava/util/ArrayList;->size()I

    move-result v2

    if-lez v2, :cond_11

    const/4 v2, 0x0

    .line 155
    new-array v2, v2, [Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/util/ArrayList;->toArray([Ljava/lang/Object;)[Ljava/lang/Object;

    move-result-object v1

    check-cast v1, [Ljava/lang/String;

    const-string v2, "tags"

    .line 156
    invoke-virtual {v0, v2, v1}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_11
    const-string v1, "withTagsMatching"

    .line 158
    invoke-interface {p1, v1, v0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_12
    const-string v0, "customContactUsFlows"

    .line 161
    invoke-static {p0, p1, v0}, Lcom/helpshift/util/ConfigParserUtil;->parseFlowListForKey(Landroid/content/Context;Ljava/util/Map;Ljava/lang/String;)Ljava/util/List;

    move-result-object p0

    if-eqz p0, :cond_13

    const-string v0, "customContactUsFlows"

    .line 163
    invoke-interface {p1, v0, p0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_13
    return-object p1
.end method

.method public static parseFlowList(Landroid/content/Context;Ljava/util/List;)Ljava/util/List;
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Ljava/util/List<",
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;>;)",
            "Ljava/util/List;"
        }
    .end annotation

    .line 188
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/util/HashMap;

    const-string v2, "dynamicFormFlow"

    const-string v3, "type"

    .line 189
    invoke-virtual {v1, v3}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/String;

    invoke-virtual {v2, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_0

    const-string v2, "data"

    .line 190
    invoke-static {p0, v1, v2}, Lcom/helpshift/util/ConfigParserUtil;->parseFlowListForKey(Landroid/content/Context;Ljava/util/Map;Ljava/lang/String;)Ljava/util/List;

    goto :goto_0

    :cond_0
    const-string v2, "config"

    .line 193
    invoke-virtual {v1, v2}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/util/HashMap;

    .line 194
    invoke-static {p0, v2}, Lcom/helpshift/util/ConfigParserUtil;->parseConfigDictionary(Landroid/content/Context;Ljava/util/Map;)Ljava/util/Map;

    move-result-object v2

    const-string v3, "config"

    .line 195
    invoke-virtual {v1, v3, v2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    .line 198
    :cond_1
    invoke-static {p0, p1}, Lcom/helpshift/support/util/DynamicFormUtil;->toFlowList(Landroid/content/Context;Ljava/util/List;)Ljava/util/List;

    move-result-object p0

    return-object p0
.end method

.method private static parseFlowListForKey(Landroid/content/Context;Ljava/util/Map;Ljava/lang/String;)Ljava/util/List;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;",
            "Ljava/lang/String;",
            ")",
            "Ljava/util/List;"
        }
    .end annotation

    const/4 v0, 0x0

    .line 173
    :try_start_0
    invoke-interface {p1, p2}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/util/List;
    :try_end_0
    .catch Ljava/lang/ClassCastException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    .line 176
    sget-object p2, Lcom/helpshift/util/ConfigParserUtil;->TAG:Ljava/lang/String;

    const-string v1, "parseFlowListForKey"

    invoke-static {p2, v1, p1}, Landroid/util/Log;->i(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    move-object p1, v0

    :goto_0
    if-eqz p1, :cond_0

    .line 180
    invoke-static {p0, p1}, Lcom/helpshift/util/ConfigParserUtil;->parseFlowList(Landroid/content/Context;Ljava/util/List;)Ljava/util/List;

    move-result-object p0

    return-object p0

    :cond_0
    return-object v0
.end method

.method public static parseMetaDataMap(Ljava/lang/String;)Ljava/util/HashMap;
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    .line 32
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0, p0}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    .line 33
    invoke-static {v0}, Lcom/helpshift/util/HSJSONUtils;->toMap(Lorg/json/JSONObject;)Ljava/util/HashMap;

    move-result-object p0

    .line 34
    invoke-static {p0}, Lcom/helpshift/util/ConfigParserUtil;->fixMetaData(Ljava/util/HashMap;)V

    return-object p0
.end method

.method private static replaceWithBoolean(Ljava/util/HashSet;Ljava/util/Map;)Ljava/util/Map;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/HashSet<",
            "Ljava/lang/String;",
            ">;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation

    .line 202
    invoke-virtual {p0}, Ljava/util/HashSet;->iterator()Ljava/util/Iterator;

    move-result-object p0

    .line 203
    :cond_0
    :goto_0
    invoke-interface {p0}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_2

    .line 204
    invoke-interface {p0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    .line 205
    invoke-interface {p1, v0}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    .line 206
    instance-of v2, v1, Ljava/lang/String;

    if-eqz v2, :cond_0

    const-string v2, "yes"

    .line 207
    invoke-virtual {v1, v2}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_1

    const/4 v1, 0x1

    .line 208
    invoke-static {v1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v1

    invoke-interface {p1, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    :cond_1
    const-string v2, "no"

    .line 210
    invoke-virtual {v1, v2}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_0

    const/4 v1, 0x0

    .line 211
    invoke-static {v1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v1

    invoke-interface {p1, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    :cond_2
    return-object p1
.end method
