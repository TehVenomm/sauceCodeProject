.class Lcom/crashlytics/android/core/CrashlyticsCore$7;
.super Ljava/lang/Object;
.source "CrashlyticsCore.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/crashlytics/android/core/CrashlyticsCore;->getSendDecisionFromUser(Landroid/app/Activity;Lio/fabric/sdk/android/services/settings/PromptSettingsData;)Z
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/crashlytics/android/core/CrashlyticsCore;

.field final synthetic val$activity:Landroid/app/Activity;

.field final synthetic val$latch:Lcom/crashlytics/android/core/CrashlyticsCore$OptInLatch;

.field final synthetic val$promptData:Lio/fabric/sdk/android/services/settings/PromptSettingsData;

.field final synthetic val$stringResolver:Lcom/crashlytics/android/core/DialogStringResolver;


# direct methods
.method constructor <init>(Lcom/crashlytics/android/core/CrashlyticsCore;Landroid/app/Activity;Lcom/crashlytics/android/core/CrashlyticsCore$OptInLatch;Lcom/crashlytics/android/core/DialogStringResolver;Lio/fabric/sdk/android/services/settings/PromptSettingsData;)V
    .locals 0

    .line 942
    iput-object p1, p0, Lcom/crashlytics/android/core/CrashlyticsCore$7;->this$0:Lcom/crashlytics/android/core/CrashlyticsCore;

    iput-object p2, p0, Lcom/crashlytics/android/core/CrashlyticsCore$7;->val$activity:Landroid/app/Activity;

    iput-object p3, p0, Lcom/crashlytics/android/core/CrashlyticsCore$7;->val$latch:Lcom/crashlytics/android/core/CrashlyticsCore$OptInLatch;

    iput-object p4, p0, Lcom/crashlytics/android/core/CrashlyticsCore$7;->val$stringResolver:Lcom/crashlytics/android/core/DialogStringResolver;

    iput-object p5, p0, Lcom/crashlytics/android/core/CrashlyticsCore$7;->val$promptData:Lio/fabric/sdk/android/services/settings/PromptSettingsData;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 11

    .line 945
    new-instance v0, Landroid/app/AlertDialog$Builder;

    iget-object v1, p0, Lcom/crashlytics/android/core/CrashlyticsCore$7;->val$activity:Landroid/app/Activity;

    invoke-direct {v0, v1}, Landroid/app/AlertDialog$Builder;-><init>(Landroid/content/Context;)V

    .line 947
    new-instance v1, Lcom/crashlytics/android/core/CrashlyticsCore$7$1;

    invoke-direct {v1, p0}, Lcom/crashlytics/android/core/CrashlyticsCore$7$1;-><init>(Lcom/crashlytics/android/core/CrashlyticsCore$7;)V

    .line 955
    iget-object v2, p0, Lcom/crashlytics/android/core/CrashlyticsCore$7;->val$activity:Landroid/app/Activity;

    invoke-virtual {v2}, Landroid/app/Activity;->getResources()Landroid/content/res/Resources;

    move-result-object v2

    invoke-virtual {v2}, Landroid/content/res/Resources;->getDisplayMetrics()Landroid/util/DisplayMetrics;

    move-result-object v2

    iget v2, v2, Landroid/util/DisplayMetrics;->density:F

    .line 959
    iget-object v3, p0, Lcom/crashlytics/android/core/CrashlyticsCore$7;->this$0:Lcom/crashlytics/android/core/CrashlyticsCore;

    const/4 v4, 0x5

    invoke-static {v3, v2, v4}, Lcom/crashlytics/android/core/CrashlyticsCore;->access$300(Lcom/crashlytics/android/core/CrashlyticsCore;FI)I

    move-result v3

    .line 961
    new-instance v4, Landroid/widget/TextView;

    iget-object v5, p0, Lcom/crashlytics/android/core/CrashlyticsCore$7;->val$activity:Landroid/app/Activity;

    invoke-direct {v4, v5}, Landroid/widget/TextView;-><init>(Landroid/content/Context;)V

    const/16 v5, 0xf

    .line 962
    invoke-virtual {v4, v5}, Landroid/widget/TextView;->setAutoLinkMask(I)V

    .line 963
    iget-object v5, p0, Lcom/crashlytics/android/core/CrashlyticsCore$7;->val$stringResolver:Lcom/crashlytics/android/core/DialogStringResolver;

    invoke-virtual {v5}, Lcom/crashlytics/android/core/DialogStringResolver;->getMessage()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v4, v5}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 964
    iget-object v5, p0, Lcom/crashlytics/android/core/CrashlyticsCore$7;->val$activity:Landroid/app/Activity;

    const v6, 0x1030044

    invoke-virtual {v4, v5, v6}, Landroid/widget/TextView;->setTextAppearance(Landroid/content/Context;I)V

    .line 965
    invoke-virtual {v4, v3, v3, v3, v3}, Landroid/widget/TextView;->setPadding(IIII)V

    const/4 v3, 0x0

    .line 967
    invoke-virtual {v4, v3}, Landroid/widget/TextView;->setFocusable(Z)V

    .line 969
    new-instance v5, Landroid/widget/ScrollView;

    iget-object v6, p0, Lcom/crashlytics/android/core/CrashlyticsCore$7;->val$activity:Landroid/app/Activity;

    invoke-direct {v5, v6}, Landroid/widget/ScrollView;-><init>(Landroid/content/Context;)V

    .line 970
    iget-object v6, p0, Lcom/crashlytics/android/core/CrashlyticsCore$7;->this$0:Lcom/crashlytics/android/core/CrashlyticsCore;

    const/16 v7, 0xe

    invoke-static {v6, v2, v7}, Lcom/crashlytics/android/core/CrashlyticsCore;->access$300(Lcom/crashlytics/android/core/CrashlyticsCore;FI)I

    move-result v6

    iget-object v7, p0, Lcom/crashlytics/android/core/CrashlyticsCore$7;->this$0:Lcom/crashlytics/android/core/CrashlyticsCore;

    const/4 v8, 0x2

    invoke-static {v7, v2, v8}, Lcom/crashlytics/android/core/CrashlyticsCore;->access$300(Lcom/crashlytics/android/core/CrashlyticsCore;FI)I

    move-result v7

    iget-object v8, p0, Lcom/crashlytics/android/core/CrashlyticsCore$7;->this$0:Lcom/crashlytics/android/core/CrashlyticsCore;

    const/16 v9, 0xa

    invoke-static {v8, v2, v9}, Lcom/crashlytics/android/core/CrashlyticsCore;->access$300(Lcom/crashlytics/android/core/CrashlyticsCore;FI)I

    move-result v8

    iget-object v9, p0, Lcom/crashlytics/android/core/CrashlyticsCore$7;->this$0:Lcom/crashlytics/android/core/CrashlyticsCore;

    const/16 v10, 0xc

    invoke-static {v9, v2, v10}, Lcom/crashlytics/android/core/CrashlyticsCore;->access$300(Lcom/crashlytics/android/core/CrashlyticsCore;FI)I

    move-result v2

    invoke-virtual {v5, v6, v7, v8, v2}, Landroid/widget/ScrollView;->setPadding(IIII)V

    .line 972
    invoke-virtual {v5, v4}, Landroid/widget/ScrollView;->addView(Landroid/view/View;)V

    .line 976
    invoke-virtual {v0, v5}, Landroid/app/AlertDialog$Builder;->setView(Landroid/view/View;)Landroid/app/AlertDialog$Builder;

    move-result-object v2

    iget-object v4, p0, Lcom/crashlytics/android/core/CrashlyticsCore$7;->val$stringResolver:Lcom/crashlytics/android/core/DialogStringResolver;

    invoke-virtual {v4}, Lcom/crashlytics/android/core/DialogStringResolver;->getTitle()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v2, v4}, Landroid/app/AlertDialog$Builder;->setTitle(Ljava/lang/CharSequence;)Landroid/app/AlertDialog$Builder;

    move-result-object v2

    invoke-virtual {v2, v3}, Landroid/app/AlertDialog$Builder;->setCancelable(Z)Landroid/app/AlertDialog$Builder;

    move-result-object v2

    iget-object v3, p0, Lcom/crashlytics/android/core/CrashlyticsCore$7;->val$stringResolver:Lcom/crashlytics/android/core/DialogStringResolver;

    invoke-virtual {v3}, Lcom/crashlytics/android/core/DialogStringResolver;->getSendButtonTitle()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3, v1}, Landroid/app/AlertDialog$Builder;->setNeutralButton(Ljava/lang/CharSequence;Landroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    .line 979
    iget-object v1, p0, Lcom/crashlytics/android/core/CrashlyticsCore$7;->val$promptData:Lio/fabric/sdk/android/services/settings/PromptSettingsData;

    iget-boolean v1, v1, Lio/fabric/sdk/android/services/settings/PromptSettingsData;->showCancelButton:Z

    if-eqz v1, :cond_0

    .line 980
    new-instance v1, Lcom/crashlytics/android/core/CrashlyticsCore$7$2;

    invoke-direct {v1, p0}, Lcom/crashlytics/android/core/CrashlyticsCore$7$2;-><init>(Lcom/crashlytics/android/core/CrashlyticsCore$7;)V

    .line 987
    iget-object v2, p0, Lcom/crashlytics/android/core/CrashlyticsCore$7;->val$stringResolver:Lcom/crashlytics/android/core/DialogStringResolver;

    invoke-virtual {v2}, Lcom/crashlytics/android/core/DialogStringResolver;->getCancelButtonTitle()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v2, v1}, Landroid/app/AlertDialog$Builder;->setNegativeButton(Ljava/lang/CharSequence;Landroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    .line 991
    :cond_0
    iget-object v1, p0, Lcom/crashlytics/android/core/CrashlyticsCore$7;->val$promptData:Lio/fabric/sdk/android/services/settings/PromptSettingsData;

    iget-boolean v1, v1, Lio/fabric/sdk/android/services/settings/PromptSettingsData;->showAlwaysSendButton:Z

    if-eqz v1, :cond_1

    .line 992
    new-instance v1, Lcom/crashlytics/android/core/CrashlyticsCore$7$3;

    invoke-direct {v1, p0}, Lcom/crashlytics/android/core/CrashlyticsCore$7$3;-><init>(Lcom/crashlytics/android/core/CrashlyticsCore$7;)V

    .line 1000
    iget-object v2, p0, Lcom/crashlytics/android/core/CrashlyticsCore$7;->val$stringResolver:Lcom/crashlytics/android/core/DialogStringResolver;

    invoke-virtual {v2}, Lcom/crashlytics/android/core/DialogStringResolver;->getAlwaysSendButtonTitle()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v2, v1}, Landroid/app/AlertDialog$Builder;->setPositiveButton(Ljava/lang/CharSequence;Landroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    .line 1004
    :cond_1
    invoke-virtual {v0}, Landroid/app/AlertDialog$Builder;->show()Landroid/app/AlertDialog;

    return-void
.end method
