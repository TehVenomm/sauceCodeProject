.class public final Lcom/helpshift/support/ContactUsFilter;
.super Ljava/lang/Object;
.source "ContactUsFilter.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/support/ContactUsFilter$LOCATION;
    }
.end annotation


# static fields
.field private static data:Lcom/helpshift/support/HSApiData;

.field private static enableContactUs:Ljava/lang/Integer;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 9
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static init(Landroid/content/Context;)V
    .locals 1

    .line 15
    sget-object v0, Lcom/helpshift/support/ContactUsFilter;->data:Lcom/helpshift/support/HSApiData;

    if-nez v0, :cond_0

    .line 16
    new-instance v0, Lcom/helpshift/support/HSApiData;

    invoke-direct {v0, p0}, Lcom/helpshift/support/HSApiData;-><init>(Landroid/content/Context;)V

    sput-object v0, Lcom/helpshift/support/ContactUsFilter;->data:Lcom/helpshift/support/HSApiData;

    .line 17
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object p0

    invoke-interface {p0}, Lcom/helpshift/CoreApi;->getSDKConfigurationDM()Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    move-result-object p0

    invoke-virtual {p0}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getEnableContactUs()Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;

    move-result-object p0

    invoke-virtual {p0}, Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;->getValue()I

    move-result p0

    invoke-static {p0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p0

    sput-object p0, Lcom/helpshift/support/ContactUsFilter;->enableContactUs:Ljava/lang/Integer;

    :cond_0
    return-void
.end method

.method protected static setConfig(Ljava/util/HashMap;)V
    .locals 2

    if-nez p0, :cond_0

    .line 23
    new-instance p0, Ljava/util/HashMap;

    invoke-direct {p0}, Ljava/util/HashMap;-><init>()V

    :cond_0
    const-string v0, "enableContactUs"

    .line 26
    invoke-virtual {p0, v0}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    .line 28
    instance-of v1, v0, Ljava/lang/Integer;

    if-eqz v1, :cond_1

    const-string v0, "enableContactUs"

    .line 29
    invoke-virtual {p0, v0}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p0

    check-cast p0, Ljava/lang/Integer;

    sput-object p0, Lcom/helpshift/support/ContactUsFilter;->enableContactUs:Ljava/lang/Integer;

    goto :goto_0

    .line 31
    :cond_1
    instance-of p0, v0, Ljava/lang/Boolean;

    if-eqz p0, :cond_3

    .line 32
    check-cast v0, Ljava/lang/Boolean;

    invoke-virtual {v0}, Ljava/lang/Boolean;->booleanValue()Z

    move-result p0

    if-eqz p0, :cond_2

    .line 33
    sget-object p0, Lcom/helpshift/support/SupportInternal$EnableContactUs;->ALWAYS:Ljava/lang/Integer;

    sput-object p0, Lcom/helpshift/support/ContactUsFilter;->enableContactUs:Ljava/lang/Integer;

    goto :goto_0

    .line 36
    :cond_2
    sget-object p0, Lcom/helpshift/support/SupportInternal$EnableContactUs;->NEVER:Ljava/lang/Integer;

    sput-object p0, Lcom/helpshift/support/ContactUsFilter;->enableContactUs:Ljava/lang/Integer;

    :cond_3
    :goto_0
    return-void
.end method

.method public static showContactUs(Lcom/helpshift/support/ContactUsFilter$LOCATION;)Z
    .locals 4

    .line 43
    sget-object v0, Lcom/helpshift/support/ContactUsFilter$LOCATION;->SEARCH_RESULT_ACTIVITY_HEADER:Lcom/helpshift/support/ContactUsFilter$LOCATION;

    const/4 v1, 0x0

    if-eq p0, v0, :cond_8

    sget-object v0, Lcom/helpshift/support/SupportInternal$EnableContactUs;->NEVER:Ljava/lang/Integer;

    sget-object v2, Lcom/helpshift/support/ContactUsFilter;->enableContactUs:Ljava/lang/Integer;

    .line 44
    invoke-virtual {v0, v2}, Ljava/lang/Integer;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    goto :goto_1

    .line 49
    :cond_0
    sget-object v0, Lcom/helpshift/support/SupportInternal$EnableContactUs;->ALWAYS:Ljava/lang/Integer;

    sget-object v2, Lcom/helpshift/support/ContactUsFilter;->enableContactUs:Ljava/lang/Integer;

    invoke-virtual {v0, v2}, Ljava/lang/Integer;->equals(Ljava/lang/Object;)Z

    move-result v0

    const/4 v2, 0x1

    if-nez v0, :cond_7

    sget-object v0, Lcom/helpshift/support/ContactUsFilter$LOCATION;->QUESTION_FOOTER:Lcom/helpshift/support/ContactUsFilter$LOCATION;

    if-ne p0, v0, :cond_1

    goto :goto_0

    .line 55
    :cond_1
    sget-object v0, Lcom/helpshift/support/ContactUsFilter$LOCATION;->ACTION_BAR:Lcom/helpshift/support/ContactUsFilter$LOCATION;

    if-ne p0, v0, :cond_3

    .line 56
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object p0

    invoke-interface {p0}, Lcom/helpshift/CoreApi;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object p0

    if-eqz p0, :cond_2

    const/4 v1, 0x1

    :cond_2
    return v1

    .line 61
    :cond_3
    sget-object v0, Lcom/helpshift/support/SupportInternal$EnableContactUs;->AFTER_VIEWING_FAQS:Ljava/lang/Integer;

    sget-object v3, Lcom/helpshift/support/ContactUsFilter;->enableContactUs:Ljava/lang/Integer;

    invoke-virtual {v0, v3}, Ljava/lang/Integer;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_4

    return v2

    .line 65
    :cond_4
    sget-object v0, Lcom/helpshift/support/SupportInternal$EnableContactUs;->AFTER_MARKING_ANSWER_UNHELPFUL:Ljava/lang/Integer;

    sget-object v3, Lcom/helpshift/support/ContactUsFilter;->enableContactUs:Ljava/lang/Integer;

    invoke-virtual {v0, v3}, Ljava/lang/Integer;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_6

    .line 66
    sget-object v0, Lcom/helpshift/support/ContactUsFilter$1;->$SwitchMap$com$helpshift$support$ContactUsFilter$LOCATION:[I

    invoke-virtual {p0}, Lcom/helpshift/support/ContactUsFilter$LOCATION;->ordinal()I

    move-result p0

    aget p0, v0, p0

    packed-switch p0, :pswitch_data_0

    return v2

    .line 70
    :pswitch_0
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object p0

    invoke-interface {p0}, Lcom/helpshift/CoreApi;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object p0

    if-eqz p0, :cond_5

    const/4 v1, 0x1

    :cond_5
    return v1

    :pswitch_1
    return v1

    :cond_6
    return v2

    :cond_7
    :goto_0
    return v2

    :cond_8
    :goto_1
    return v1

    nop

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method
