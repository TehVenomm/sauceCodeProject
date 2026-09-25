.class public Lcom/helpshift/support/Support;
.super Ljava/lang/Object;
.source "Support.java"

# interfaces
.implements Lcom/helpshift/Core$ApiProvider;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/support/Support$EnableContactUs;,
        Lcom/helpshift/support/Support$RateAlert;,
        Lcom/helpshift/support/Support$LazyHolder;,
        Lcom/helpshift/support/Support$Delegate;
    }
.end annotation


# static fields
.field public static final CONVERSATION_FLOW:Ljava/lang/String; = "conversationFlow"

.field public static final CustomIssueFieldKey:Ljava/lang/String; = "hs-custom-issue-field"

.field public static final CustomMetadataKey:Ljava/lang/String; = "hs-custom-metadata"

.field public static final DYNAMIC_FORM_FLOW:Ljava/lang/String; = "dynamicFormFlow"

.field public static final FAQS_FLOW:Ljava/lang/String; = "faqsFlow"

.field public static final FAQ_SECTION_FLOW:Ljava/lang/String; = "faqSectionFlow"

.field public static final JSON_PREFS:Ljava/lang/String; = "HSJsonData"

.field public static final SINGLE_FAQ_FLOW:Ljava/lang/String; = "singleFaqFlow"

.field public static final TAG:Ljava/lang/String; = "HelpShiftDebug"

.field public static final TagsKey:Ljava/lang/String; = "hs-tags"

.field public static final UserAcceptedTheSolution:Ljava/lang/String; = "User accepted the solution"

.field public static final UserRejectedTheSolution:Ljava/lang/String; = "User rejected the solution"

.field public static final UserReviewedTheApp:Ljava/lang/String; = "User reviewed the app"

.field public static final UserSentScreenShot:Ljava/lang/String; = "User sent a screenshot"

.field public static final libraryVersion:Ljava/lang/String; = "7.6.3"


# direct methods
.method private constructor <init>()V
    .locals 0

    .line 111
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method synthetic constructor <init>(Lcom/helpshift/support/Support$1;)V
    .locals 0

    .line 31
    invoke-direct {p0}, Lcom/helpshift/support/Support;-><init>()V

    return-void
.end method

.method public static clearBreadCrumbs()V
    .locals 2

    .line 239
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->verifyInstall()Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 242
    :cond_0
    invoke-static {}, Lcom/helpshift/util/concurrent/ApiExecutorFactory;->getHandlerExecutor()Lcom/helpshift/util/concurrent/ApiExecutor;

    move-result-object v0

    .line 243
    new-instance v1, Lcom/helpshift/support/Support$4;

    invoke-direct {v1}, Lcom/helpshift/support/Support$4;-><init>()V

    invoke-interface {v0, v1}, Lcom/helpshift/util/concurrent/ApiExecutor;->runAsync(Ljava/lang/Runnable;)V

    return-void
.end method

.method public static getConversationFragment(Landroid/app/Activity;)Lcom/helpshift/support/fragments/SupportFragment;
    .locals 1
    .param p0    # Landroid/app/Activity;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 1146
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    invoke-static {p0, v0}, Lcom/helpshift/support/Support;->getConversationFragment(Landroid/app/Activity;Ljava/util/Map;)Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object p0

    return-object p0
.end method

.method public static getConversationFragment(Landroid/app/Activity;Lcom/helpshift/support/ApiConfig;)Lcom/helpshift/support/fragments/SupportFragment;
    .locals 0
    .param p0    # Landroid/app/Activity;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p1    # Lcom/helpshift/support/ApiConfig;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 1181
    invoke-static {p1}, Lcom/helpshift/support/util/ConfigUtil;->validateAndConvertToMap(Lcom/helpshift/support/ApiConfig;)Ljava/util/Map;

    move-result-object p1

    invoke-static {p0, p1}, Lcom/helpshift/support/Support;->getConversationFragment(Landroid/app/Activity;Ljava/util/Map;)Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object p0

    return-object p0
.end method

.method public static getConversationFragment(Landroid/app/Activity;Ljava/util/Map;)Lcom/helpshift/support/fragments/SupportFragment;
    .locals 1
    .param p0    # Landroid/app/Activity;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p1    # Ljava/util/Map;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/app/Activity;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)",
            "Lcom/helpshift/support/fragments/SupportFragment;"
        }
    .end annotation

    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 1162
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->verifyInstall()Z

    move-result v0

    if-nez v0, :cond_0

    const/4 p0, 0x0

    return-object p0

    .line 1165
    :cond_0
    invoke-static {}, Lcom/helpshift/util/concurrent/ApiExecutorFactory;->getHandlerExecutor()Lcom/helpshift/util/concurrent/ApiExecutor;

    move-result-object v0

    .line 1166
    invoke-interface {v0}, Lcom/helpshift/util/concurrent/ApiExecutor;->awaitForSyncExecution()V

    .line 1167
    invoke-static {p0, p1}, Lcom/helpshift/support/SupportInternal;->getConversationFragment(Landroid/app/Activity;Ljava/util/Map;)Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object p0

    return-object p0
.end method

.method public static getDynamicFormFragment(Landroid/app/Activity;Ljava/lang/String;Ljava/util/List;)Lcom/helpshift/support/fragments/SupportFragment;
    .locals 1
    .param p0    # Landroid/app/Activity;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p1    # Ljava/lang/String;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p2    # Ljava/util/List;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/app/Activity;",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Lcom/helpshift/support/flows/Flow;",
            ">;)",
            "Lcom/helpshift/support/fragments/SupportFragment;"
        }
    .end annotation

    .line 1329
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    invoke-static {p0, p1, p2, v0}, Lcom/helpshift/support/Support;->getDynamicFormFragment(Landroid/app/Activity;Ljava/lang/String;Ljava/util/List;Ljava/util/Map;)Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object p0

    return-object p0
.end method

.method public static getDynamicFormFragment(Landroid/app/Activity;Ljava/lang/String;Ljava/util/List;Lcom/helpshift/support/ApiConfig;)Lcom/helpshift/support/fragments/SupportFragment;
    .locals 0
    .param p0    # Landroid/app/Activity;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p1    # Ljava/lang/String;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p2    # Ljava/util/List;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p3    # Lcom/helpshift/support/ApiConfig;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/app/Activity;",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Lcom/helpshift/support/flows/Flow;",
            ">;",
            "Lcom/helpshift/support/ApiConfig;",
            ")",
            "Lcom/helpshift/support/fragments/SupportFragment;"
        }
    .end annotation

    .line 1398
    invoke-static {p3}, Lcom/helpshift/support/util/ConfigUtil;->validateAndConvertToMap(Lcom/helpshift/support/ApiConfig;)Ljava/util/Map;

    move-result-object p3

    invoke-static {p0, p1, p2, p3}, Lcom/helpshift/support/Support;->getDynamicFormFragment(Landroid/app/Activity;Ljava/lang/String;Ljava/util/List;Ljava/util/Map;)Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object p0

    return-object p0
.end method

.method private static getDynamicFormFragment(Landroid/app/Activity;Ljava/lang/String;Ljava/util/List;Ljava/util/Map;)Lcom/helpshift/support/fragments/SupportFragment;
    .locals 1
    .param p0    # Landroid/app/Activity;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p1    # Ljava/lang/String;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p2    # Ljava/util/List;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p3    # Ljava/util/Map;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/app/Activity;",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Lcom/helpshift/support/flows/Flow;",
            ">;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)",
            "Lcom/helpshift/support/fragments/SupportFragment;"
        }
    .end annotation

    .line 1404
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->verifyInstall()Z

    move-result v0

    if-nez v0, :cond_0

    const/4 p0, 0x0

    return-object p0

    .line 1407
    :cond_0
    invoke-static {}, Lcom/helpshift/util/concurrent/ApiExecutorFactory;->getHandlerExecutor()Lcom/helpshift/util/concurrent/ApiExecutor;

    move-result-object v0

    .line 1408
    invoke-interface {v0}, Lcom/helpshift/util/concurrent/ApiExecutor;->awaitForSyncExecution()V

    .line 1409
    invoke-static {p0, p1, p2, p3}, Lcom/helpshift/support/SupportInternal;->getDynamicFormFragment(Landroid/app/Activity;Ljava/lang/String;Ljava/util/List;Ljava/util/Map;)Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object p0

    return-object p0
.end method

.method public static getDynamicFormFragment(Landroid/app/Activity;Ljava/util/List;)Lcom/helpshift/support/fragments/SupportFragment;
    .locals 2
    .param p0    # Landroid/app/Activity;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p1    # Ljava/util/List;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/app/Activity;",
            "Ljava/util/List<",
            "Lcom/helpshift/support/flows/Flow;",
            ">;)",
            "Lcom/helpshift/support/fragments/SupportFragment;"
        }
    .end annotation

    const-string v0, ""

    .line 1308
    new-instance v1, Ljava/util/HashMap;

    invoke-direct {v1}, Ljava/util/HashMap;-><init>()V

    invoke-static {p0, v0, p1, v1}, Lcom/helpshift/support/Support;->getDynamicFormFragment(Landroid/app/Activity;Ljava/lang/String;Ljava/util/List;Ljava/util/Map;)Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object p0

    return-object p0
.end method

.method public static getDynamicFormFragment(Landroid/app/Activity;Ljava/util/List;Lcom/helpshift/support/ApiConfig;)Lcom/helpshift/support/fragments/SupportFragment;
    .locals 1
    .param p0    # Landroid/app/Activity;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p1    # Ljava/util/List;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p2    # Lcom/helpshift/support/ApiConfig;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/app/Activity;",
            "Ljava/util/List<",
            "Lcom/helpshift/support/flows/Flow;",
            ">;",
            "Lcom/helpshift/support/ApiConfig;",
            ")",
            "Lcom/helpshift/support/fragments/SupportFragment;"
        }
    .end annotation

    const-string v0, ""

    .line 1375
    invoke-static {p2}, Lcom/helpshift/support/util/ConfigUtil;->validateAndConvertToMap(Lcom/helpshift/support/ApiConfig;)Ljava/util/Map;

    move-result-object p2

    invoke-static {p0, v0, p1, p2}, Lcom/helpshift/support/Support;->getDynamicFormFragment(Landroid/app/Activity;Ljava/lang/String;Ljava/util/List;Ljava/util/Map;)Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object p0

    return-object p0
.end method

.method public static getDynamicFormFragment(Landroid/app/Activity;Ljava/util/List;Ljava/util/Map;)Lcom/helpshift/support/fragments/SupportFragment;
    .locals 1
    .param p0    # Landroid/app/Activity;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p1    # Ljava/util/List;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p2    # Ljava/util/Map;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/app/Activity;",
            "Ljava/util/List<",
            "Lcom/helpshift/support/flows/Flow;",
            ">;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)",
            "Lcom/helpshift/support/fragments/SupportFragment;"
        }
    .end annotation

    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    const-string v0, ""

    .line 1353
    invoke-static {p0, v0, p1, p2}, Lcom/helpshift/support/Support;->getDynamicFormFragment(Landroid/app/Activity;Ljava/lang/String;Ljava/util/List;Ljava/util/Map;)Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object p0

    return-object p0
.end method

.method public static getFAQSectionFragment(Landroid/app/Activity;Ljava/lang/String;)Lcom/helpshift/support/fragments/SupportFragment;
    .locals 1
    .param p0    # Landroid/app/Activity;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p1    # Ljava/lang/String;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 1195
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    invoke-static {p0, p1, v0}, Lcom/helpshift/support/Support;->getFAQSectionFragment(Landroid/app/Activity;Ljava/lang/String;Ljava/util/Map;)Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object p0

    return-object p0
.end method

.method public static getFAQSectionFragment(Landroid/app/Activity;Ljava/lang/String;Lcom/helpshift/support/ApiConfig;)Lcom/helpshift/support/fragments/SupportFragment;
    .locals 0
    .param p0    # Landroid/app/Activity;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p1    # Ljava/lang/String;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p2    # Lcom/helpshift/support/ApiConfig;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 1235
    invoke-static {p2}, Lcom/helpshift/support/util/ConfigUtil;->validateAndConvertToMap(Lcom/helpshift/support/ApiConfig;)Ljava/util/Map;

    move-result-object p2

    invoke-static {p0, p1, p2}, Lcom/helpshift/support/Support;->getFAQSectionFragment(Landroid/app/Activity;Ljava/lang/String;Ljava/util/Map;)Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object p0

    return-object p0
.end method

.method public static getFAQSectionFragment(Landroid/app/Activity;Ljava/lang/String;Ljava/util/Map;)Lcom/helpshift/support/fragments/SupportFragment;
    .locals 1
    .param p0    # Landroid/app/Activity;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p1    # Ljava/lang/String;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p2    # Ljava/util/Map;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/app/Activity;",
            "Ljava/lang/String;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)",
            "Lcom/helpshift/support/fragments/SupportFragment;"
        }
    .end annotation

    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 1213
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->verifyInstall()Z

    move-result v0

    if-nez v0, :cond_0

    const/4 p0, 0x0

    return-object p0

    .line 1216
    :cond_0
    invoke-static {}, Lcom/helpshift/util/concurrent/ApiExecutorFactory;->getHandlerExecutor()Lcom/helpshift/util/concurrent/ApiExecutor;

    move-result-object v0

    .line 1217
    invoke-interface {v0}, Lcom/helpshift/util/concurrent/ApiExecutor;->awaitForSyncExecution()V

    .line 1218
    invoke-static {p0, p1, p2}, Lcom/helpshift/support/SupportInternal;->getFAQSectionFragment(Landroid/app/Activity;Ljava/lang/String;Ljava/util/Map;)Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object p0

    return-object p0
.end method

.method public static getFAQsFragment(Landroid/app/Activity;)Lcom/helpshift/support/fragments/SupportFragment;
    .locals 1
    .param p0    # Landroid/app/Activity;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 1100
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    invoke-static {p0, v0}, Lcom/helpshift/support/Support;->getFAQsFragment(Landroid/app/Activity;Ljava/util/Map;)Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object p0

    return-object p0
.end method

.method public static getFAQsFragment(Landroid/app/Activity;Lcom/helpshift/support/ApiConfig;)Lcom/helpshift/support/fragments/SupportFragment;
    .locals 0
    .param p0    # Landroid/app/Activity;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p1    # Lcom/helpshift/support/ApiConfig;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 1134
    invoke-static {p1}, Lcom/helpshift/support/util/ConfigUtil;->validateAndConvertToMap(Lcom/helpshift/support/ApiConfig;)Ljava/util/Map;

    move-result-object p1

    invoke-static {p0, p1}, Lcom/helpshift/support/Support;->getFAQsFragment(Landroid/app/Activity;Ljava/util/Map;)Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object p0

    return-object p0
.end method

.method public static getFAQsFragment(Landroid/app/Activity;Ljava/util/Map;)Lcom/helpshift/support/fragments/SupportFragment;
    .locals 1
    .param p0    # Landroid/app/Activity;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p1    # Ljava/util/Map;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/app/Activity;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)",
            "Lcom/helpshift/support/fragments/SupportFragment;"
        }
    .end annotation

    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 1116
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->verifyInstall()Z

    move-result v0

    if-nez v0, :cond_0

    const/4 p0, 0x0

    return-object p0

    .line 1119
    :cond_0
    invoke-static {}, Lcom/helpshift/util/concurrent/ApiExecutorFactory;->getHandlerExecutor()Lcom/helpshift/util/concurrent/ApiExecutor;

    move-result-object v0

    .line 1120
    invoke-interface {v0}, Lcom/helpshift/util/concurrent/ApiExecutor;->awaitForSyncExecution()V

    .line 1121
    invoke-static {p0, p1}, Lcom/helpshift/support/SupportInternal;->getFAQsFragment(Landroid/app/Activity;Ljava/util/Map;)Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object p0

    return-object p0
.end method

.method public static getInstance()Lcom/helpshift/support/Support;
    .locals 1

    .line 120
    sget-object v0, Lcom/helpshift/support/Support$LazyHolder;->INSTANCE:Lcom/helpshift/support/Support;

    return-object v0
.end method

.method public static getNotificationCount()Ljava/lang/Integer;
    .locals 1

    .line 133
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->verifyInstall()Z

    move-result v0

    if-nez v0, :cond_0

    const/4 v0, -0x1

    .line 134
    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    return-object v0

    .line 136
    :cond_0
    invoke-static {}, Lcom/helpshift/util/concurrent/ApiExecutorFactory;->getHandlerExecutor()Lcom/helpshift/util/concurrent/ApiExecutor;

    move-result-object v0

    .line 137
    invoke-interface {v0}, Lcom/helpshift/util/concurrent/ApiExecutor;->awaitForSyncExecution()V

    .line 138
    invoke-static {}, Lcom/helpshift/support/SupportInternal;->getNotificationCount()Ljava/lang/Integer;

    move-result-object v0

    return-object v0
.end method

.method public static getNotificationCount(Landroid/os/Handler;Landroid/os/Handler;)V
    .locals 2

    .line 167
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->verifyInstall()Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 170
    :cond_0
    invoke-static {}, Lcom/helpshift/util/concurrent/ApiExecutorFactory;->getHandlerExecutor()Lcom/helpshift/util/concurrent/ApiExecutor;

    move-result-object v0

    .line 171
    new-instance v1, Lcom/helpshift/support/Support$1;

    invoke-direct {v1, p0, p1}, Lcom/helpshift/support/Support$1;-><init>(Landroid/os/Handler;Landroid/os/Handler;)V

    invoke-interface {v0, v1}, Lcom/helpshift/util/concurrent/ApiExecutor;->runAsync(Ljava/lang/Runnable;)V

    return-void
.end method

.method public static getSingleFAQFragment(Landroid/app/Activity;Ljava/lang/String;)Lcom/helpshift/support/fragments/SupportFragment;
    .locals 1
    .param p0    # Landroid/app/Activity;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p1    # Ljava/lang/String;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 1249
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    invoke-static {p0, p1, v0}, Lcom/helpshift/support/Support;->getSingleFAQFragment(Landroid/app/Activity;Ljava/lang/String;Ljava/util/Map;)Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object p0

    return-object p0
.end method

.method public static getSingleFAQFragment(Landroid/app/Activity;Ljava/lang/String;Lcom/helpshift/support/ApiConfig;)Lcom/helpshift/support/fragments/SupportFragment;
    .locals 0
    .param p0    # Landroid/app/Activity;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p1    # Ljava/lang/String;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p2    # Lcom/helpshift/support/ApiConfig;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 1288
    invoke-static {p2}, Lcom/helpshift/support/util/ConfigUtil;->validateAndConvertToMap(Lcom/helpshift/support/ApiConfig;)Ljava/util/Map;

    move-result-object p2

    invoke-static {p0, p1, p2}, Lcom/helpshift/support/Support;->getSingleFAQFragment(Landroid/app/Activity;Ljava/lang/String;Ljava/util/Map;)Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object p0

    return-object p0
.end method

.method public static getSingleFAQFragment(Landroid/app/Activity;Ljava/lang/String;Ljava/util/Map;)Lcom/helpshift/support/fragments/SupportFragment;
    .locals 1
    .param p0    # Landroid/app/Activity;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p1    # Ljava/lang/String;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p2    # Ljava/util/Map;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/app/Activity;",
            "Ljava/lang/String;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)",
            "Lcom/helpshift/support/fragments/SupportFragment;"
        }
    .end annotation

    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 1267
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->verifyInstall()Z

    move-result v0

    if-nez v0, :cond_0

    const/4 p0, 0x0

    return-object p0

    .line 1270
    :cond_0
    invoke-static {}, Lcom/helpshift/util/concurrent/ApiExecutorFactory;->getHandlerExecutor()Lcom/helpshift/util/concurrent/ApiExecutor;

    move-result-object v0

    .line 1271
    invoke-interface {v0}, Lcom/helpshift/util/concurrent/ApiExecutor;->awaitForSyncExecution()V

    .line 1272
    invoke-static {p0, p1, p2}, Lcom/helpshift/support/SupportInternal;->getSingleFAQFragment(Landroid/app/Activity;Ljava/lang/String;Ljava/util/Map;)Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object p0

    return-object p0
.end method

.method public static isConversationActive()Z
    .locals 1

    .line 263
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->verifyInstall()Z

    move-result v0

    if-nez v0, :cond_0

    const/4 v0, 0x0

    return v0

    .line 266
    :cond_0
    invoke-static {}, Lcom/helpshift/util/concurrent/ApiExecutorFactory;->getHandlerExecutor()Lcom/helpshift/util/concurrent/ApiExecutor;

    move-result-object v0

    .line 267
    invoke-interface {v0}, Lcom/helpshift/util/concurrent/ApiExecutor;->awaitForSyncExecution()V

    .line 268
    invoke-static {}, Lcom/helpshift/support/SupportInternal;->isConversationActive()Z

    move-result v0

    return v0
.end method

.method public static leaveBreadCrumb(Ljava/lang/String;)V
    .locals 2

    .line 214
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->verifyInstall()Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 217
    :cond_0
    invoke-static {}, Lcom/helpshift/util/concurrent/ApiExecutorFactory;->getHandlerExecutor()Lcom/helpshift/util/concurrent/ApiExecutor;

    move-result-object v0

    .line 218
    new-instance v1, Lcom/helpshift/support/Support$3;

    invoke-direct {v1, p0}, Lcom/helpshift/support/Support$3;-><init>(Ljava/lang/String;)V

    invoke-interface {v0, v1}, Lcom/helpshift/util/concurrent/ApiExecutor;->runAsync(Ljava/lang/Runnable;)V

    return-void
.end method

.method public static setDelegate(Lcom/helpshift/support/Support$Delegate;)V
    .locals 2

    .line 999
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->verifyInstall()Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 1002
    :cond_0
    invoke-static {}, Lcom/helpshift/util/concurrent/ApiExecutorFactory;->getHandlerExecutor()Lcom/helpshift/util/concurrent/ApiExecutor;

    move-result-object v0

    .line 1003
    new-instance v1, Lcom/helpshift/support/Support$12;

    invoke-direct {v1, p0}, Lcom/helpshift/support/Support$12;-><init>(Lcom/helpshift/support/Support$Delegate;)V

    invoke-interface {v0, v1}, Lcom/helpshift/util/concurrent/ApiExecutor;->runAsync(Ljava/lang/Runnable;)V

    return-void
.end method

.method public static setMetadataCallback(Lcom/helpshift/support/Callable;)V
    .locals 2
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 899
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->verifyInstall()Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 902
    :cond_0
    invoke-static {}, Lcom/helpshift/util/concurrent/ApiExecutorFactory;->getHandlerExecutor()Lcom/helpshift/util/concurrent/ApiExecutor;

    move-result-object v0

    .line 903
    new-instance v1, Lcom/helpshift/support/Support$9;

    invoke-direct {v1, p0}, Lcom/helpshift/support/Support$9;-><init>(Lcom/helpshift/support/Callable;)V

    invoke-interface {v0, v1}, Lcom/helpshift/util/concurrent/ApiExecutor;->runAsync(Ljava/lang/Runnable;)V

    return-void
.end method

.method public static setMetadataCallback(Lcom/helpshift/support/MetadataCallable;)V
    .locals 2

    .line 932
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->verifyInstall()Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 935
    :cond_0
    invoke-static {}, Lcom/helpshift/util/concurrent/ApiExecutorFactory;->getHandlerExecutor()Lcom/helpshift/util/concurrent/ApiExecutor;

    move-result-object v0

    .line 936
    new-instance v1, Lcom/helpshift/support/Support$10;

    invoke-direct {v1, p0}, Lcom/helpshift/support/Support$10;-><init>(Lcom/helpshift/support/MetadataCallable;)V

    invoke-interface {v0, v1}, Lcom/helpshift/util/concurrent/ApiExecutor;->runAsync(Ljava/lang/Runnable;)V

    return-void
.end method

.method public static setSDKLanguage(Ljava/lang/String;)V
    .locals 2

    .line 1035
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->verifyInstall()Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 1038
    :cond_0
    invoke-static {}, Lcom/helpshift/util/concurrent/ApiExecutorFactory;->getHandlerExecutor()Lcom/helpshift/util/concurrent/ApiExecutor;

    move-result-object v0

    .line 1039
    new-instance v1, Lcom/helpshift/support/Support$13;

    invoke-direct {v1, p0}, Lcom/helpshift/support/Support$13;-><init>(Ljava/lang/String;)V

    invoke-interface {v0, v1}, Lcom/helpshift/util/concurrent/ApiExecutor;->runAsync(Ljava/lang/Runnable;)V

    return-void
.end method

.method public static setUserIdentifier(Ljava/lang/String;)V
    .locals 2
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 190
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->verifyInstall()Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 193
    :cond_0
    invoke-static {}, Lcom/helpshift/util/concurrent/ApiExecutorFactory;->getHandlerExecutor()Lcom/helpshift/util/concurrent/ApiExecutor;

    move-result-object v0

    .line 194
    new-instance v1, Lcom/helpshift/support/Support$2;

    invoke-direct {v1, p0}, Lcom/helpshift/support/Support$2;-><init>(Ljava/lang/String;)V

    invoke-interface {v0, v1}, Lcom/helpshift/util/concurrent/ApiExecutor;->runAsync(Ljava/lang/Runnable;)V

    return-void
.end method

.method public static showAlertToRateApp(Ljava/lang/String;Lcom/helpshift/support/AlertToRateAppListener;)V
    .locals 2

    .line 981
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->verifyInstall()Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 984
    :cond_0
    invoke-static {}, Lcom/helpshift/util/concurrent/ApiExecutorFactory;->getHandlerExecutor()Lcom/helpshift/util/concurrent/ApiExecutor;

    move-result-object v0

    .line 985
    new-instance v1, Lcom/helpshift/support/Support$11;

    invoke-direct {v1, p0, p1}, Lcom/helpshift/support/Support$11;-><init>(Ljava/lang/String;Lcom/helpshift/support/AlertToRateAppListener;)V

    invoke-interface {v0, v1}, Lcom/helpshift/util/concurrent/ApiExecutor;->runOnUiThread(Ljava/lang/Runnable;)V

    return-void
.end method

.method public static showConversation(Landroid/app/Activity;)V
    .locals 1

    .line 288
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    invoke-static {p0, v0}, Lcom/helpshift/support/Support;->showConversation(Landroid/app/Activity;Ljava/util/Map;)V

    return-void
.end method

.method public static showConversation(Landroid/app/Activity;Lcom/helpshift/support/ApiConfig;)V
    .locals 0
    .param p0    # Landroid/app/Activity;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p1    # Lcom/helpshift/support/ApiConfig;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 408
    invoke-static {p1}, Lcom/helpshift/support/util/ConfigUtil;->validateAndConvertToMap(Lcom/helpshift/support/ApiConfig;)Ljava/util/Map;

    move-result-object p1

    invoke-static {p0, p1}, Lcom/helpshift/support/Support;->showConversation(Landroid/app/Activity;Ljava/util/Map;)V

    return-void
.end method

.method public static showConversation(Landroid/app/Activity;Ljava/util/Map;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/app/Activity;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)V"
        }
    .end annotation

    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 359
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->verifyInstall()Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 362
    :cond_0
    invoke-static {}, Lcom/helpshift/util/concurrent/ApiExecutorFactory;->getHandlerExecutor()Lcom/helpshift/util/concurrent/ApiExecutor;

    move-result-object v0

    .line 363
    new-instance v1, Lcom/helpshift/support/Support$5;

    invoke-direct {v1, p0, p1}, Lcom/helpshift/support/Support$5;-><init>(Landroid/app/Activity;Ljava/util/Map;)V

    invoke-interface {v0, v1}, Lcom/helpshift/util/concurrent/ApiExecutor;->runOnUiThread(Ljava/lang/Runnable;)V

    return-void
.end method

.method public static showDynamicForm(Landroid/app/Activity;Ljava/lang/String;Ljava/util/List;)V
    .locals 2
    .param p0    # Landroid/app/Activity;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p1    # Ljava/lang/String;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p2    # Ljava/util/List;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/app/Activity;",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Lcom/helpshift/support/flows/Flow;",
            ">;)V"
        }
    .end annotation

    .line 1079
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->verifyInstall()Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 1082
    :cond_0
    invoke-static {}, Lcom/helpshift/util/concurrent/ApiExecutorFactory;->getHandlerExecutor()Lcom/helpshift/util/concurrent/ApiExecutor;

    move-result-object v0

    .line 1083
    new-instance v1, Lcom/helpshift/support/Support$14;

    invoke-direct {v1, p0, p1, p2}, Lcom/helpshift/support/Support$14;-><init>(Landroid/app/Activity;Ljava/lang/String;Ljava/util/List;)V

    invoke-interface {v0, v1}, Lcom/helpshift/util/concurrent/ApiExecutor;->runOnUiThread(Ljava/lang/Runnable;)V

    return-void
.end method

.method public static showDynamicForm(Landroid/app/Activity;Ljava/util/List;)V
    .locals 1
    .param p0    # Landroid/app/Activity;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p1    # Ljava/util/List;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/app/Activity;",
            "Ljava/util/List<",
            "Lcom/helpshift/support/flows/Flow;",
            ">;)V"
        }
    .end annotation

    const-string v0, ""

    .line 1060
    invoke-static {p0, v0, p1}, Lcom/helpshift/support/Support;->showDynamicForm(Landroid/app/Activity;Ljava/lang/String;Ljava/util/List;)V

    return-void
.end method

.method public static showFAQSection(Landroid/app/Activity;Ljava/lang/String;)V
    .locals 1

    .line 434
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    invoke-static {p0, p1, v0}, Lcom/helpshift/support/Support;->showFAQSection(Landroid/app/Activity;Ljava/lang/String;Ljava/util/Map;)V

    return-void
.end method

.method public static showFAQSection(Landroid/app/Activity;Ljava/lang/String;Lcom/helpshift/support/ApiConfig;)V
    .locals 1
    .param p0    # Landroid/app/Activity;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p1    # Ljava/lang/String;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p2    # Lcom/helpshift/support/ApiConfig;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 572
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->verifyInstall()Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 575
    :cond_0
    invoke-static {p2}, Lcom/helpshift/support/util/ConfigUtil;->validateAndConvertToMap(Lcom/helpshift/support/ApiConfig;)Ljava/util/Map;

    move-result-object p2

    invoke-static {p0, p1, p2}, Lcom/helpshift/support/Support;->showFAQSection(Landroid/app/Activity;Ljava/lang/String;Ljava/util/Map;)V

    return-void
.end method

.method public static showFAQSection(Landroid/app/Activity;Ljava/lang/String;Ljava/util/Map;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/app/Activity;",
            "Ljava/lang/String;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)V"
        }
    .end annotation

    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 517
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->verifyInstall()Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 520
    :cond_0
    invoke-static {}, Lcom/helpshift/util/concurrent/ApiExecutorFactory;->getHandlerExecutor()Lcom/helpshift/util/concurrent/ApiExecutor;

    move-result-object v0

    .line 521
    new-instance v1, Lcom/helpshift/support/Support$6;

    invoke-direct {v1, p0, p1, p2}, Lcom/helpshift/support/Support$6;-><init>(Landroid/app/Activity;Ljava/lang/String;Ljava/util/Map;)V

    invoke-interface {v0, v1}, Lcom/helpshift/util/concurrent/ApiExecutor;->runOnUiThread(Ljava/lang/Runnable;)V

    return-void
.end method

.method public static showFAQs(Landroid/app/Activity;)V
    .locals 1

    .line 745
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    invoke-static {p0, v0}, Lcom/helpshift/support/Support;->showFAQs(Landroid/app/Activity;Ljava/util/Map;)V

    return-void
.end method

.method public static showFAQs(Landroid/app/Activity;Lcom/helpshift/support/ApiConfig;)V
    .locals 0
    .param p0    # Landroid/app/Activity;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p1    # Lcom/helpshift/support/ApiConfig;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 871
    invoke-static {p1}, Lcom/helpshift/support/util/ConfigUtil;->validateAndConvertToMap(Lcom/helpshift/support/ApiConfig;)Ljava/util/Map;

    move-result-object p1

    invoke-static {p0, p1}, Lcom/helpshift/support/Support;->showFAQs(Landroid/app/Activity;Ljava/util/Map;)V

    return-void
.end method

.method public static showFAQs(Landroid/app/Activity;Ljava/util/Map;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/app/Activity;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)V"
        }
    .end annotation

    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 822
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->verifyInstall()Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 825
    :cond_0
    invoke-static {}, Lcom/helpshift/util/concurrent/ApiExecutorFactory;->getHandlerExecutor()Lcom/helpshift/util/concurrent/ApiExecutor;

    move-result-object v0

    .line 826
    new-instance v1, Lcom/helpshift/support/Support$8;

    invoke-direct {v1, p0, p1}, Lcom/helpshift/support/Support$8;-><init>(Landroid/app/Activity;Ljava/util/Map;)V

    invoke-interface {v0, v1}, Lcom/helpshift/util/concurrent/ApiExecutor;->runOnUiThread(Ljava/lang/Runnable;)V

    return-void
.end method

.method public static showSingleFAQ(Landroid/app/Activity;Ljava/lang/String;)V
    .locals 1

    .line 598
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    invoke-static {p0, p1, v0}, Lcom/helpshift/support/Support;->showSingleFAQ(Landroid/app/Activity;Ljava/lang/String;Ljava/util/Map;)V

    return-void
.end method

.method public static showSingleFAQ(Landroid/app/Activity;Ljava/lang/String;Lcom/helpshift/support/ApiConfig;)V
    .locals 0
    .param p0    # Landroid/app/Activity;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p1    # Ljava/lang/String;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p2    # Lcom/helpshift/support/ApiConfig;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 725
    invoke-static {p2}, Lcom/helpshift/support/util/ConfigUtil;->validateAndConvertToMap(Lcom/helpshift/support/ApiConfig;)Ljava/util/Map;

    move-result-object p2

    invoke-static {p0, p1, p2}, Lcom/helpshift/support/Support;->showSingleFAQ(Landroid/app/Activity;Ljava/lang/String;Ljava/util/Map;)V

    return-void
.end method

.method public static showSingleFAQ(Landroid/app/Activity;Ljava/lang/String;Ljava/util/Map;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/app/Activity;",
            "Ljava/lang/String;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)V"
        }
    .end annotation

    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 671
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->verifyInstall()Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 674
    :cond_0
    invoke-static {}, Lcom/helpshift/util/concurrent/ApiExecutorFactory;->getHandlerExecutor()Lcom/helpshift/util/concurrent/ApiExecutor;

    move-result-object v0

    .line 675
    new-instance v1, Lcom/helpshift/support/Support$7;

    invoke-direct {v1, p0, p1, p2}, Lcom/helpshift/support/Support$7;-><init>(Landroid/app/Activity;Ljava/lang/String;Ljava/util/Map;)V

    invoke-interface {v0, v1}, Lcom/helpshift/util/concurrent/ApiExecutor;->runOnUiThread(Ljava/lang/Runnable;)V

    return-void
.end method


# virtual methods
.method public _clearAnonymousUser()Z
    .locals 1

    .line 1489
    invoke-static {}, Lcom/helpshift/support/SupportInternal;->clearAnonymousUser()Z

    move-result v0

    return v0
.end method

.method public _getActionExecutor()Lcom/helpshift/executors/ActionExecutor;
    .locals 1

    const/4 v0, 0x0

    return-object v0
.end method

.method public _handlePush(Landroid/content/Context;Landroid/content/Intent;)V
    .locals 0

    .line 1465
    invoke-static {p1, p2}, Lcom/helpshift/support/SupportInternal;->handlePush(Landroid/content/Context;Landroid/content/Intent;)V

    return-void
.end method

.method public _install(Landroid/app/Application;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 0
    .param p1    # Landroid/app/Application;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p2    # Ljava/lang/String;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p3    # Ljava/lang/String;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p4    # Ljava/lang/String;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 1429
    invoke-static {p1, p2, p3, p4}, Lcom/helpshift/support/SupportInternal;->install(Landroid/app/Application;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public _install(Landroid/app/Application;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)V
    .locals 0
    .param p1    # Landroid/app/Application;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p2    # Ljava/lang/String;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p3    # Ljava/lang/String;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p4    # Ljava/lang/String;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p5    # Ljava/util/Map;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/app/Application;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)V"
        }
    .end annotation

    .line 1441
    invoke-static {p1, p2, p3, p4, p5}, Lcom/helpshift/support/SupportInternal;->install(Landroid/app/Application;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)V

    return-void
.end method

.method public _login(Lcom/helpshift/HelpshiftUser;)Z
    .locals 0

    .line 1473
    invoke-static {p1}, Lcom/helpshift/support/SupportInternal;->login(Lcom/helpshift/HelpshiftUser;)Z

    move-result p1

    return p1
.end method

.method public _logout()Z
    .locals 1

    .line 1481
    invoke-static {}, Lcom/helpshift/support/SupportInternal;->logout()Z

    move-result v0

    return v0
.end method

.method public _preInstall(Landroid/app/Application;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/app/Application;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)V"
        }
    .end annotation

    .line 1418
    invoke-static {p1, p2, p3, p4, p5}, Lcom/helpshift/support/SupportInternal;->preInstall(Landroid/app/Application;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)V

    return-void
.end method

.method public _registerDeviceToken(Landroid/content/Context;Ljava/lang/String;)V
    .locals 0
    .param p1    # Landroid/content/Context;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p2    # Ljava/lang/String;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 1457
    invoke-static {p1, p2}, Lcom/helpshift/support/SupportInternal;->registerDeviceToken(Landroid/content/Context;Ljava/lang/String;)V

    return-void
.end method

.method public _setNameAndEmail(Ljava/lang/String;Ljava/lang/String;)V
    .locals 0

    .line 1449
    invoke-static {p1, p2}, Lcom/helpshift/support/SupportInternal;->setNameAndEmail(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public _setSDKLanguage(Ljava/lang/String;)V
    .locals 0

    .line 1505
    invoke-static {p1}, Lcom/helpshift/support/Support;->setSDKLanguage(Ljava/lang/String;)V

    return-void
.end method

.method public _setTheme(I)V
    .locals 2

    .line 1510
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->verifyInstall()Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 1513
    :cond_0
    invoke-static {}, Lcom/helpshift/util/concurrent/ApiExecutorFactory;->getHandlerExecutor()Lcom/helpshift/util/concurrent/ApiExecutor;

    move-result-object v0

    .line 1514
    new-instance v1, Lcom/helpshift/support/Support$15;

    invoke-direct {v1, p0, p1}, Lcom/helpshift/support/Support$15;-><init>(Lcom/helpshift/support/Support;I)V

    invoke-interface {v0, v1}, Lcom/helpshift/util/concurrent/ApiExecutor;->runAsync(Ljava/lang/Runnable;)V

    return-void
.end method
