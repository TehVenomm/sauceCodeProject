.class public final Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM$FAQ;
.super Ljava/lang/Object;
.source "FAQListMessageDM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x19
    name = "FAQ"
.end annotation


# instance fields
.field public final language:Ljava/lang/String;

.field public final publishId:Ljava/lang/String;

.field public final title:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 0

    .line 148
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 149
    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM$FAQ;->title:Ljava/lang/String;

    .line 150
    iput-object p2, p0, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM$FAQ;->publishId:Ljava/lang/String;

    .line 151
    iput-object p3, p0, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM$FAQ;->language:Ljava/lang/String;

    return-void
.end method
